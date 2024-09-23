using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface IOrderItemsRepository
    {
        Task<IEnumerable<OrderItems>> GetOrderItemsAsync();
        Task<OrderItems> GetOrderItemsByIdAsync(int orderId, int productId);
        Task AddOrderItemsAsync(OrderItems orderItems);
        Task UpdateOrderItemsAsync(OrderItems orderItems);
        Task DeleteOrderItemsAsync(int orderId, int productId);
    }
}
