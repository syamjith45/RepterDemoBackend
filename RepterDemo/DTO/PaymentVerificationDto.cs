namespace RepterDemo.DTO
{
    public class PaymentVerificationDto
    {
        public string PaymentId { get; set; }
        public string OrderId { get; set; }
        public string Signature { get; set; }
    }
}
