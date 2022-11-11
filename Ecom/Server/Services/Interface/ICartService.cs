using Ecom.Shared;
using Ecom.Shared.Dto;

namespace Ecom.Server.Services.Interface
{
    public interface ICartService
    {
        Task<ServiceResponse<List<CartProductResponseDto>>> GetAllCartItemsAsync(List<Cart> cartItems);
    }
}
