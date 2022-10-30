using Ecom.Shared;
using Ecom.Shared.Dto;
using System.Net.Http.Json;

namespace Ecom.Client.Services.ProductService
{
    public class ProductService : IProductService
    {
        public List<Product> Products { get; set; }
        public string Message { get; set; } = "Loading Products";
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; } = 0;
        public string LastSearchText { get; set; } =  string.Empty;

        private readonly HttpClient _http;

        public event Action ProductsChanged;

        public ProductService(HttpClient http)
        {
            _http = http;
        }


        public async Task GetProducts(string? category = null)
        {
            var response = new ServiceResponse<List<Product>>();
            response = category == null ? await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product/getFeaturedProducts") : await _http.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/Category/{category}");
           
            if(response != null && response.Data != null) Products = response.Data;

            ProductsChanged.Invoke();

        }

        public async Task<Product> GetProductById(int id)
        {
            var results = await _http.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{id}");
            return results.Data;
        }

        public async Task SearchProducts(string searchText, int id)
        {
            LastSearchText = searchText;
            var results = await _http.GetFromJsonAsync<ServiceResponse<ProductSearchResultsDto>>($"api/Product/search/{searchText}/{id}");
            CurrentPage = 1;
            if (results != null && results.Data != null)
            {
                Products = results.Data.Products;
                CurrentPage = results.Data.CurrentPage;
                PageCount = results.Data.TotalPages;
            }
            
            if(Products.Count() == 0)
            {
                Message = "No Products found!";
            }

            ProductsChanged.Invoke();
        }

        public async Task<List<string>> GetProductSearchSuggestions(string text)
        {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/searchsuggestions/{text}");
            return result.Data;
        }
    }
}
