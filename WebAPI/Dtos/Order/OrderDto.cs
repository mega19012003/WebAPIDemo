namespace WebAPI.Dtos.Order
{
    public class OrderDto
    {
        public Guid TableId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
