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
            var products = await _context.Products.ToListAsync();

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
            var product = await _context.Products.FindAsync(id);
            var response = new ServiceResponse<Product>();

            if (product == null) return response = new ServiceResponse<Product> { Success = false, Message = $"No Product Found with Id={id}", Status = 404 };

            return response = new ServiceResponse<Product> { Success = true, Data=product, Message = $"Product Found with Id={id}", Status = 200 };

        }
    }
}
