using Ecom.Server.Data;
using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext _context;

        public ProductService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Product>>> GetAllProductsAsync()
        {
            var products = await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Variants)
                .ThenInclude(v => v.ProductType)
                .ToListAsync();

            var response = new ServiceResponse<List<Product>>
            {
                Data = products,
                Status = 200,
                Success = true
                
            };

            return response;
        }

        public async Task<ServiceResponse<Product>> GetProductAsync(int id)
        {
            var product = await _context.Products
                .Include(c => c.Category)
                .Include(p => p.Variants)
                .ThenInclude(p => p.ProductType)
                .SingleOrDefaultAsync(x => x.Id == id);
            var response = new ServiceResponse<Product>();

            if (product == null) return response = new ServiceResponse<Product> 
            { 
                Success = false, 
                Message = $"No Product Found with Id={id}", 
                Status = 404 
            };

            return response = new ServiceResponse<Product> 
            { Success = true, 
                Data=product, 
                Message = $"Product Found with Id={id}", 
                Status = 200 
            };

        }

        public async Task<ServiceResponse<List<Product>>> GetProductsByCategoryAsync(string categoryName)
        {
            var products = await _context.Products
                .Include(c=> c.Category)
                .Include(p => p.Variants)
                .ThenInclude(p => p.ProductType)
                .Where(c => c.Category.Url == categoryName.ToLower())
                .ToListAsync();

            var response = new ServiceResponse<List<Product>>();

            if(products.Count() == 0 || products == null)
            {
                response.Status = 404;
                response.Success = false;
                response.Message = "No Products with the specific category found!";

                return response;
            }

            response.Data = products;
            response.Status = 200;
            response.Success = true;
            response.Message = "Products with the specific category found!";
            return response;
        }
    }
}
