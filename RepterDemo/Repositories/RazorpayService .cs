using Razorpay.Api;
using System.Security.Cryptography;
using System.Text;

namespace RepterDemo.Repositories
{
    public class RazorpayService : IRazorpayService
    {
        private readonly string _key = "rzp_test_pqbYiV78bUPOqe"; // Razorpay test key
        private readonly string _secret = "adTbWlL6Q8IVNwLJepPm04ME"; // Razorpay secret

        // Generate a dynamic receipt ID for the transaction
        public string GenerateReceiptId()
        {
            string timestamp = DateTime.Now.ToString("yyyyMMddHHmmss"); // e.g., 20240909123456
            string randomString = Guid.NewGuid().ToString().Substring(0, 8); // e.g., 8-character random string
            return $"receipt_{timestamp}_{randomString}";
        }

        // Create a new Razorpay order
        public async Task<Order> CreateOrderAsync(decimal amount, string currency = "INR")
        {
            try
            {
                string receiptId = GenerateReceiptId();

                var options = new Dictionary<string, object>
        {
            { "amount", (amount * 100) }, // Amount in paise
            { "currency", currency },
            { "receipt", receiptId },
            { "payment_capture", 1 } // Auto capture payment
        };

                var client = new RazorpayClient(_key, _secret);
                var order = client.Order.Create(options);

                // Log the order details to check if Razorpay is returning valid data
                Console.WriteLine($"Razorpay Order: {order}");

                return await Task.FromResult(order);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error creating Razorpay order: {ex.Message}");
                throw;
            }
        }


        // Verify payment by comparing the signature from Razorpay
        public async Task<bool> VerifyPaymentAsync(string paymentId, string orderId, string signature)
        {
            try
            {
                // Create the payload for signature
                string payload = $"{orderId}|{paymentId}";

                // Generate signature using the Razorpay secret
                string generatedSignature = GenerateSignature(payload, _secret); // Use secret, not key

                // Compare signatures
                return await Task.FromResult(generatedSignature == signature);
            }
            catch (Exception)
            {
                return false; // Return false in case of any error
            }
        }

        // Generate HMAC SHA256 signature for data using the secret key
        private string GenerateSignature(string data, string key)
        {
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(key))) // Use the secret key here
            {
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(hash).Replace("-", "").ToLower(); // Convert to hex string
            }
        }
    }
}
