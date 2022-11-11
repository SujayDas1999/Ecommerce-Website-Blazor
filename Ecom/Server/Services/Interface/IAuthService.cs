using Ecom.Shared;

namespace Ecom.Server.Services.Interface
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(string email, string password);
        Task<bool> UserExists(string email);
    }
}
