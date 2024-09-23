using RepterDemo.DTO;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public interface IShippingDetailsService
    {
        Task<ShippingDetails> AddShippingDetailsAsync(ShippingDetailsDto shippingDetailsDto);
        Task<ShippingDetails> GetShippingDetailsByUserIdAsync(int userId);
    }
}
