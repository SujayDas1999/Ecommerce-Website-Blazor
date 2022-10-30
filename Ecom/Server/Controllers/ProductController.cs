using Ecom.Server.Data;
using Ecom.Server.Services;
using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Ecom.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Product>>> GetProductAsync(int id)
        {
            var product = await _productService.GetProductAsync(id);
            return Ok(product);
        }

        [HttpGet("category/{categoryurl}")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductsByCategoriesAsync(string categoryurl)
        {
            var productsByCategories = await _productService.GetProductsByCategoryAsync(categoryurl);
            return productsByCategories;
        }

        [HttpGet("search/{searchText}/{page}")]
        public async Task<ActionResult<ServiceResponse<ProductSearchResultsDto>>> GetSearchedProductAsync(string searchText, int page = 1)
        {
            return Ok(await _productService.GetSearchedProducts(searchText, page));
        }

        [HttpGet("searchsuggestions/{searchText}")]
        public async Task<ActionResult<ServiceResponse<List<string>>>> GetProductSearchSuggestion(string searchText)
        {
            return Ok(await _productService.GetProductSearchSuggestions(searchText));
        }

        [HttpGet("getFeaturedProducts")]
        public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProducts()
        {
            return Ok(await _productService.GetFeaturedProducts());
        }

    }
}
