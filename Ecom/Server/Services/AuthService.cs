using Ecom.Server.Data;
using Ecom.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Unicode;

namespace Ecom.Server.Services.Interface
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _config;

        public AuthService(DataContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<ServiceResponse<string>> Login(string email, string password)
        {
            var response = new ServiceResponse<string>();
           

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email.ToLower() == email.ToLower());

            if(user == null)
            {
                response.Success = false;
                response.Message = "User not found!";
                response.Status = 401;
                return response;
            }
            else if(!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            {
                response.Success = false;
                response.Message = "Invalid Username or Password!";
                response.Status = 401;
                return response;
            }
            else
            {
                response.Data = CreateToken(user);
                response.Message = "User logged in successfully!";
                response.Status = 200;
                response.Success = true;
                return response;
            }
            

        }

        public async Task<ServiceResponse<int>> Register(string email, string password)
        {
            var result = new ServiceResponse<int>
            {
                Data = 0
            };

            if(await UserExists(email))
            {
                result.Success = false;
                result.Message = "User already exists";
                result.Status = 400;

                return result;
            }
            else
            {
                CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

                var user = new User
                {
                    Email = email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                result.Data = user.Id;
                result.Success = true;
                result.Message = "Successfully registered an user";
                result.Status = 200;

                return result;
            }
        }

        public async Task<bool> UserExists(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email.ToLower() == email.ToLower());    
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            using (var hmac = new HMACSHA512(PasswordSalt))
            {
                var computedHash =  hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(PasswordHash);
            }
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
                );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(int userId, string password)
        {
            var user = await _context.Users.FindAsync(userId);
            if(user == null)
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Status = 401,
                    Success = false,
                    Message = "User Not Found!"
                };
            }

            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "Password has been changed!",
                Success = true,
                Status = 200
            };
        }
    }
}
