using Ecom.Server.Data;
using Ecom.Shared;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace Ecom.Server.Services.Interface
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;

        public AuthService(DataContext context)
        {
            _context = context;
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
    }
}
