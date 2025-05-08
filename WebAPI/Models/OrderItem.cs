using System.ComponentModel.DataAnnotations;

namespace WebAPI.Models
{
    public class OrderItem
    {
        public Guid OrderItemId { get; set; }
        //public Guid OrderId { get; set; }
        public Guid OrderFoodId { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
        public bool isCancelled { get; set; }
    }
}
