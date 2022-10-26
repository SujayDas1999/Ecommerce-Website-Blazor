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
    }
}
