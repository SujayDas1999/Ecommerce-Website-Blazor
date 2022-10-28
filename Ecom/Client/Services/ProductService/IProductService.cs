using Ecom.Shared;

namespace Ecom.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Product> Products { get; set; }
        Task GetProducts(string? category = null);
        Task<Product> GetProductById(int id);
        
    }
}
