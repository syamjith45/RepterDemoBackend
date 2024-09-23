using Razorpay.Api;

namespace RepterDemo.Repositories
{
    public interface IRazorpayService
    {
        Task<Order> CreateOrderAsync(decimal amount, string currency);
        Task<bool> VerifyPaymentAsync(string paymentId, string orderId, string signature);
    }
}
