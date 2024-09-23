namespace RepterDemo.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }

        public User User { get; set; }
        public List<OrderItems> OrderItems { get; set; }
    }
}
