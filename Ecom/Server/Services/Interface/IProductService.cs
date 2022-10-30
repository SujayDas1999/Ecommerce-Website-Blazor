using Ecom.Shared;
using Ecom.Shared.Dto;

namespace Ecom.Server.Services.Interface
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetAllProductsAsync();
        Task<ServiceResponse<Product>> GetProductAsync(int id);
        Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryName);
        Task<ServiceResponse<ProductSearchResultsDto>> GetSearchedProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();

    }
}
