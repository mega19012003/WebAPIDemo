using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Order
{
    public class PutOrderItemDto
    {
        public Guid OrderFoodId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public bool isCancelled { get; set; }
        [Required]
        public decimal Subtotal { get; set; }
    }
}
