using Ecom.Server.Data;
using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Ecom.Shared.Dto;
using Microsoft.EntityFrameworkCore;

namespace Ecom.Server.Services
{
    public class CartService : ICartService
    {
        private readonly DataContext _context;

        public CartService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<CartProductResponseDto>>> GetAllCartItemsAsync(List<Cart> cartItems)
        {
            var result = new ServiceResponse<List<CartProductResponseDto>>
            {
                Data = new List<CartProductResponseDto>()
            };

            foreach (var item in cartItems)
            {
                var product = await _context.Products.Where(id => item.ProductId == id.Id).FirstOrDefaultAsync();

                if(product == null)
                {
                    continue;
                }

                var productVariant = await _context.ProductVariants.
                    Where(v => v.ProductId == item.ProductId && v.ProductTypeId == item.ProductTypeId)
                    .Include(v => v.ProductType)
                    .FirstOrDefaultAsync();
                
                if (productVariant == null)
                {
                    continue;
                }

                var cartProduct = new CartProductResponseDto
                {
                    ProductId = product.Id,
                    Title = product.Title,
                    ImageUrl = product.ImageUrl,
                    Price = productVariant.Price,
                    ProductType = productVariant.ProductType.Name,
                    ProductTypeId = productVariant.ProductTypeId,
                    Quantity = item.Quantity,
                };

                result.Data.Add(cartProduct);
            }

            return result;
        }
    }
}
