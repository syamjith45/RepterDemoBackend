namespace RepterDemo.Models
{
    public class ShippingDetails
    {
        public int ShippingDetailsID { get; set; }
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
    }
}
