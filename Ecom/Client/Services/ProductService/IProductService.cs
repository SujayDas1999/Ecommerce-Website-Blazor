using Ecom.Shared;

namespace Ecom.Client.Services.ProductService
{
    public interface IProductService
    {
        List<Product> Products { get; set; }
        Task<List<Product>> GetProducts();
        Task<Product> GetProductById(int id);
    }
}
