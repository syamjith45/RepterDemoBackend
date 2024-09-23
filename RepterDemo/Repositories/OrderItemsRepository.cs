using Microsoft.EntityFrameworkCore;
using RepterDemo.Data;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public class OrderItemsRepository : IOrderItemsRepository
    {
        private readonly AppDbContext _context;

        public OrderItemsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItems>> GetOrderItemsAsync()
        {
            return await _context.OrderItems.Include(oi => oi.Product).ToListAsync();
        }

        public async Task<OrderItems> GetOrderItemsByIdAsync(int orderId, int productId)
        {
            return await _context.OrderItems.Include(oi => oi.Product)
                                            .FirstOrDefaultAsync(oi => oi.OrderID == orderId && oi.ProductID == productId);
        }

        public async Task AddOrderItemsAsync(OrderItems orderItems)
        {
            _context.OrderItems.Add(orderItems);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderItemsAsync(OrderItems orderItems)
        {
            _context.Entry(orderItems).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderItemsAsync(int orderId, int productId)
        {
            var orderItems = await _context.OrderItems.FindAsync(orderId, productId);
            _context.OrderItems.Remove(orderItems);
            await _context.SaveChangesAsync();
        }
    }
}
