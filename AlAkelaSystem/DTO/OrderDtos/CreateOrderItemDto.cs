namespace DTO.OrderDtos
{
    public class CreateOrderItemDto
    {
        public int productId { get; set; }
        public int quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}