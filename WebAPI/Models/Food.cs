using System.ComponentModel.Design;
using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Food
    {
        public Guid Id { get; set; } //= Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Price { get; set; }
        public List<string> ImgUrl { get; set; } = new List<string>();
        public Guid MenuId { get; set; } 
    }
}
