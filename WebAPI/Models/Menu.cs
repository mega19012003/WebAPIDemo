using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class Menu
    {
        public Guid MenuId { get; set; } //= Guid.NewGuid().ToString();
        public string Name { get; set; }

    }
}
