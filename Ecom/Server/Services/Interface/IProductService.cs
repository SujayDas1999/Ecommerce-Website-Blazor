using Ecom.Shared;

namespace Ecom.Server.Services.Interface
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetAllProductsAsync();
    }
}
