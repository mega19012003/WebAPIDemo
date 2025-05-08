using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Auth
{
    public class RegisterDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Fullname { get; set; }
    }
}
