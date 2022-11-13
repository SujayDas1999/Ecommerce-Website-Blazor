using Ecom.Shared;

namespace Ecom.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> RegisterUser(UserRegister user); 
        Task<ServiceResponse<string>> LoginUser(UserLogin user);
        Task<ServiceResponse<bool>> ChangePassword(UserPasswordChange userPasswordChange);
    }
}
