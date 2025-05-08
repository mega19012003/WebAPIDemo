using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Order
{
    public class PutOrderDto
    {
        public Guid OrderId { get; set; }
        [Required]
        public bool Status { get; set; }
    }
}
