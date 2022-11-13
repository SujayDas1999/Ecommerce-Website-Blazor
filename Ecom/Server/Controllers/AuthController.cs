using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ecom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<int>>> RegisterUser(UserRegister user)
        {
            return Ok( await _authService.Register(user.Email, user.Password));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginUser(UserLogin user)
        {
            return Ok(await _authService.Login(user.Email, user.Password));
        }

        [HttpPost("changePassword"), Authorize]
        public async Task<ActionResult<ServiceResponse<bool>>> ChangePassword(string password)
        {
            var user = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var response = await _authService.ChangePassword(int.Parse(user), password);

           if(!response.Success)
            {
                return Unauthorized(response);
            }
            else
            {
                return Ok(response);
            }
        }
        

    }
}
