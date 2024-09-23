namespace RepterDemo.DTO
{
    public class CreateOrderDto
    {
        // The amount for the order, required by Razorpay for order creation
        public decimal Amount { get; set; }

        // You can add more properties if needed, such as currency, description, etc.
        public string Currency { get; set; } = "INR"; // Defaulting to INR
 
    }
}

