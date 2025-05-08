using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Order
{
    public class OrderItemDto
    {
        public Guid FoodId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
    }
}
