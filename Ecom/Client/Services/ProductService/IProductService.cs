using Ecom.Shared;

namespace Ecom.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductsChanged;
        List<Product> Products { get; set; }
        int CurrentPage { get; set; }
        int PageCount { get; set; }
        string LastSearchText { get; set; }
        string Message { get; set; }
        Task GetProducts(string? category = null);
        Task<Product> GetProductById(int id);
        Task SearchProducts(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string text);
    }
}
