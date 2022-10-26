using Ecom.Server.Data;
using Ecom.Server.Services;
using Ecom.Server.Services.Interface;
using Ecom.Shared;
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

    }
}
