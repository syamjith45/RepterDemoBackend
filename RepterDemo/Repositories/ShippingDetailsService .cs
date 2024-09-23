using Microsoft.EntityFrameworkCore;
using RepterDemo.Data;
using RepterDemo.DTO;
using RepterDemo.Models;

namespace RepterDemo.Repositories
{
    public class ShippingDetailsService : IShippingDetailsService
    {
        private readonly AppDbContext _context;

        public ShippingDetailsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShippingDetails> AddShippingDetailsAsync(ShippingDetailsDto shippingDetailsDto)
        {
            var shippingDetails = new ShippingDetails
            {
                UserID = shippingDetailsDto.UserID,
                FullName = shippingDetailsDto.FullName,
                Address = shippingDetailsDto.Address,
                City = shippingDetailsDto.City,
                State = shippingDetailsDto.State,
                ZipCode = shippingDetailsDto.ZipCode,
                PhoneNumber = shippingDetailsDto.PhoneNumber
            };

            _context.ShippingDetails.Add(shippingDetails);
            await _context.SaveChangesAsync();

            return shippingDetails;
        }

        public async Task<ShippingDetails> GetShippingDetailsByUserIdAsync(int userId)
        {
            return await _context.ShippingDetails
                .FirstOrDefaultAsync(sd => sd.UserID == userId);
        }
    }

}
