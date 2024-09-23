using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RepterDemo.DTO;
using RepterDemo.Repositories;

namespace RepterDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RazorpayController : ControllerBase
    {
        private readonly IRazorpayService _razorpayService;



        public RazorpayController(IRazorpayService razorpayService)
        {
            _razorpayService = razorpayService;
        }

        [HttpPost("create-order")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto createOrderDto)
        {
            try
            {
                // Create an order using the Razorpay service
                var order = await _razorpayService.CreateOrderAsync(createOrderDto.Amount, createOrderDto.Currency);

                // Access properties of the 'Order' object
                var orderId = order["id"].ToString();
                var amount = Convert.ToInt32(order["amount"]);
                var currency = order["currency"].ToString();

                // Return a response with the Razorpay key and order details
                return Ok(new
                {
                    orderId = orderId,
                    amount = amount,
                    currency = currency,
                    key = "rzp_test_pqbYiV78bUPOqe" // You can replace this with your actual Razorpay key
                });
            }
            catch (Exception ex)
            {
                // Log the exception and return an error message
                Console.WriteLine($"Error creating Razorpay order: {ex.Message}");
                return StatusCode(500, new { message = "Failed to create order with Razorpay." });
            }
        }




        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] PaymentVerificationDto verificationDto)
        {
            bool isPaymentValid = await _razorpayService.VerifyPaymentAsync(verificationDto.PaymentId, verificationDto.OrderId, verificationDto.Signature);

            if (isPaymentValid)
            {
                // Process the order and update the order status
                return Ok(new { success = true, message = "Payment verified successfully." });
            }
            else
            {
                return BadRequest(new { success = false, message = "Payment verification failed." });
            }
        }

    }
}
