using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public bool Status { get; set; } = false;
        public List<OrderItem> OrderItems { get; set; }
    }
}
