namespace RepterDemo.DTO
{
    public class CartSummaryDto
    {
        public List<CartItemDto> Items { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
