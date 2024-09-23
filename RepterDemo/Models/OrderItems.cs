using System.ComponentModel.DataAnnotations;

namespace RepterDemo.Models
{
    public class OrderItems
    {
        [Key]  // Add this attribute to denote the primary key
        public int OrderItemID { get; set; }  // Add a primary key property

        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }

        public Order Order { get; set; }
        public Product Product { get; set; }
    }

}
