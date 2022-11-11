using Ecom.Shared;
using Ecom.Shared.Dto;

namespace Ecom.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(Cart cart);
        Task<List<Cart>> GetAllItems();
        Task<List<CartProductResponseDto>> GetCartProducts();
        Task RemoveProductFromCart(int prouctId, int productTypeId);
        Task UpdateQuantity(CartProductResponseDto product);
    }
}
