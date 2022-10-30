using Blazored.LocalStorage;
using Ecom.Shared;

namespace Ecom.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;

        public CartService(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public event Action OnChange;

        public async Task AddToCart(Cart cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");
            if(cart == null)
            {
                cart = new List<Cart>();
            }
            cart.Add(cartItem);

            await _localStorage.SetItemAsync("cart", cart);
        }

        public async Task<List<Cart>> GetAllItems()
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");
            if (cart == null)
            {
                cart = new List<Cart>();
            }

            return cart;
        }
    }
}
