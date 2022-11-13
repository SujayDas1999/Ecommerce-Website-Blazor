using Ecom.Shared;
using System.Net.Http.Json;

namespace Ecom.Client.Services.AuthService
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        public AuthService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ServiceResponse<bool>> ChangePassword(UserPasswordChange request)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/changePassword", request.Password);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
            return result;
        }

        public async Task<ServiceResponse<string>> LoginUser(UserLogin user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/login", user);
            var result = await response.Content.ReadFromJsonAsync<ServiceResponse<string>>();
            return result;
        }

        public async Task<ServiceResponse<int>> RegisterUser(UserRegister user)
        {
            var response = await _httpClient.PostAsJsonAsync("api/auth/register",user);
            var result =  await response.Content.ReadFromJsonAsync<ServiceResponse<int>>();
            return result;
        }
    }
}
