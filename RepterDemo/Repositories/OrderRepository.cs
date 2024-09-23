using Microsoft.EntityFrameworkCore;
using RepterDemo.Data;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{

    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            return await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.OrderItems)
                                        .FirstOrDefaultAsync(o => o.OrderID == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateOrderAsync(Order order)
        {
            _context.Entry(order).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
