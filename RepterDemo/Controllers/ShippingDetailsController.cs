using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepterDemo.DTO;
using RepterDemo.Repositories;

namespace RepterDemo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ShippingDetailsController : ControllerBase
    {
        private readonly IShippingDetailsService _shippingDetailsService;

        public ShippingDetailsController(IShippingDetailsService shippingDetailsService)
        {
            _shippingDetailsService = shippingDetailsService;
        }

        // POST: api/ShippingDetails
        [HttpPost]
        public async Task<IActionResult> AddShippingDetails([FromBody] ShippingDetailsDto shippingDetailsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var shippingDetails = await _shippingDetailsService.AddShippingDetailsAsync(shippingDetailsDto);

            return Ok(shippingDetails);
        }

        // GET: api/ShippingDetails/{userId}
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetShippingDetails(int userId)
        {
            var shippingDetails = await _shippingDetailsService.GetShippingDetailsByUserIdAsync(userId);

            if (shippingDetails == null)
            {
                return NotFound("Shipping details not found");
            }

            return Ok(shippingDetails);
        }
    }

}
