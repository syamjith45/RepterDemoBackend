using RepterDemo.DTO;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIDAsync(int userId);
        Task AddToCartAsync(int userId, AddToCartDTO addToCartDto);
        Task RemoveFromCartAsync(int cartItemId);
        Task UpdateCartAsync(int cartItemId, int quantity);
    }
}
