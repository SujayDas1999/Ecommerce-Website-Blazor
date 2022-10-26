using Ecom.Shared;
using System.Net.Http.Json;

namespace Ecom.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        public List<Product> Products { get; set; }

        private readonly HttpClient _http;

        public ProductService(HttpClient http)
        {
            _http = http;
        }


        public async Task<List<Product>> GetProducts()
        {
            var results = await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product");
            return results.Data;
        }
    }
}
