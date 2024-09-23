namespace RepterDemo.Models
{
    public class CartItem
    {
        public int CartItemID { get; set; }
        public int CartID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; } // Make Product nullable if it's not always required
    }

}
