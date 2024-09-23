using Microsoft.AspNetCore.Mvc;
using RepterDemo.DTO;
using RepterDemo.Models;
using RepterDemo.Repositories;

namespace RepterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<Cart>> GetCart(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIDAsync(userId);
            return Ok(cart);
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> AddToCart(int userId, AddToCartDTO addToCartDto)
        {
            await _cartRepository.AddToCartAsync(userId, addToCartDto);
            return Ok("Item added to cart");
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            await _cartRepository.RemoveFromCartAsync(cartItemId);
            return Ok("Item removed from cart");
        }

        [HttpPut("{cartItemId}")]
        public async Task<IActionResult> UpdateCart(int cartItemId, int quantity)
        {
            await _cartRepository.UpdateCartAsync(cartItemId, quantity);
            return Ok("Cart updated");
        }
    }

}
