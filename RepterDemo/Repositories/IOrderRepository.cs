using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);
    }
}
