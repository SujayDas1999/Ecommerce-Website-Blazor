using Blazored.LocalStorage;
using Ecom.Shared;
using Ecom.Shared.Dto;
using System.Net.Http.Json;

namespace Ecom.Client.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly HttpClient _http;

        public CartService(ILocalStorageService localStorage, HttpClient http)
        {
            _localStorage = localStorage;
            _http = http;
        }

        public event Action OnChange;

        public async Task AddToCart(Cart cartItem)
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");
            if(cart == null)
            {
                cart = new List<Cart>();
            }

            var sameItem = cart.Find(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
            if(sameItem == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameItem.Quantity += cartItem.Quantity;
            }

            await _localStorage.SetItemAsync("cart", cart);
            OnChange.Invoke();
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

        public async Task<List<CartProductResponseDto>> GetCartProducts()
        {
            var cartItems = await _localStorage.GetItemAsync<List<Cart>>("cart");
            var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);
            var cartProducts =
                await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponseDto>>>();

            return cartProducts?.Data!;
        }

        public async Task RemoveProductFromCart(int prouctId, int productTypeId)
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");
            if (cart == null) return;

            var cartItem = cart.Find(x => x.ProductId == prouctId && x.ProductTypeId == productTypeId);
            if(cartItem != null)
            {
                cart.Remove(cartItem);
                await _localStorage.SetItemAsync("cart",cart);
                OnChange.Invoke();
            }
        }

        public async Task UpdateQuantity(CartProductResponseDto product)
        {
            var cart = await _localStorage.GetItemAsync<List<Cart>>("cart");
            if (cart == null) return;

            var cartItem = cart.Find(x => x.ProductId == product.ProductId && x.ProductTypeId == product.ProductTypeId);
            if (cartItem != null)
            {
                cartItem.Quantity = product.Quantity;
                await _localStorage.SetItemAsync("cart", cart);
            }
        }
    }
}
