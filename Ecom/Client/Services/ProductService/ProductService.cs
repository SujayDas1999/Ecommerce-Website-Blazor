using Ecom.Shared;
using System.Net.Http.Json;

namespace Ecom.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        public List<Product> Products { get; set; }

        private readonly HttpClient _http;

        public event Action ProductsChanged;

        public ProductService(HttpClient http)
        {
            _http = http;
        }


        public async Task GetProducts(string? category = null)
        {
            var response = new ServiceResponse<List<Product>>();
            response = category == null ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product") : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/Category/{category}");
           
            if(response != null && response.Data != null) Products = response.Data;

            ProductsChanged.Invoke();

        }

        public async Task<Product> GetProductById(int id)
        {
            var results = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{id}");
            return results.Data;
        }
    }
}
