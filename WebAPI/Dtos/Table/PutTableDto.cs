using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Table
{
    public class PutTableDto
    {
        public Guid TableId { get; set; }
        [Required]
        public string TableName { get; set; }
        [Required]
        public int Seats { get; set; }
    }
}
