using Ecom.Server.Services.Interface;
using Ecom.Shared;
using Ecom.Shared.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecom.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("products")]
        public async Task<ActionResult<ServiceResponse<List<CartProductResponseDto>>>> GetCartProducts(List<Cart> cartItems)
        {
            var result = await _cartService.GetAllCartItemsAsync(cartItems);
            return Ok(result);
        }
    }
}
