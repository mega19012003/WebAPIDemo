using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Table
{
    public class PostTableDto
    {
        [Required]
        public string TableName { get; set; }
        [Required]
        public int Seats { get; set; }
    }
}
