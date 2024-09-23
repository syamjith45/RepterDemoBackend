using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RepterDemo.Data;
using RepterDemo.Models;

using RepterDemo.DTO;
using RepterDemo.Repositories;

namespace RepterDemo.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context;

        public CartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Cart> GetCartByUserIDAsync(int userId)
        {
            return await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId);
        }

        public async Task AddToCartAsync(int userId, AddToCartDTO addToCartDto)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserID = userId,
                    CreatedAt = DateTime.Now,
                    CartItems = new List<CartItem>()
                };
                _context.Cart.Add(cart);
            }

            var existingCartItem = cart.CartItems.FirstOrDefault(ci => ci.ProductID == addToCartDto.ProductID);

            var product = await _context.Products.FindAsync(addToCartDto.ProductID); // Fetch the Product

            if (existingCartItem != null)
            {
                existingCartItem.Quantity += addToCartDto.Quantity;
            }
            else
            {
                var newCartItem = new CartItem
                {
                    ProductID = addToCartDto.ProductID,
                    Quantity = addToCartDto.Quantity,
                    CartID = cart.CartID,
                    Product = product // Set the Product property here
                };
                cart.CartItems.Add(newCartItem);
            }

            await _context.SaveChangesAsync();
        }

        public async Task RemoveFromCartAsync(int cartItemId)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateCartAsync(int cartItemId, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(cartItemId);
            if (cartItem != null)
            {
                cartItem.Quantity = quantity;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CartSummaryDto> GetCartSummaryAsync(int userId)
        {
            var cart = await _context.Cart
                .Include(c => c.CartItems)
                .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserID == userId);

            if (cart == null)
                return null;

            var cartSummary = new CartSummaryDto
            {
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    ProductID = ci.ProductID,
                    ProductName = ci.Product.Name,
                    Quantity = ci.Quantity,
                    Price = ci.Product.Price,
                    TotalPrice = ci.Quantity * ci.Product.Price
                }).ToList(),
                TotalAmount = cart.CartItems.Sum(ci => ci.Quantity * ci.Product.Price)
            };

            return cartSummary;
        }

    }
}
