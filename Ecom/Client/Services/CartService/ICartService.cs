using Ecom.Shared;

namespace Ecom.Client.Services.CartService
{
    public interface ICartService
    {
        event Action OnChange;
        Task AddToCart(Cart cart);
        Task<List<Cart>> GetAllItems();
    }
}
