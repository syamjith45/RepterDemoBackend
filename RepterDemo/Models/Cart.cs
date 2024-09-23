namespace RepterDemo.Models
{
    public class Cart
    {

        public int CartID { get; set; }
        public int UserID { get; set; }
        public DateTime CreatedAt { get; set; }
        public required List<CartItem> CartItems { get; set; }
    }
}
