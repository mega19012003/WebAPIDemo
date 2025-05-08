using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Food
{
    public class PostFoodDto
    {
        public Guid MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public List<IFormFile> Img { get; set; }
    }
}
