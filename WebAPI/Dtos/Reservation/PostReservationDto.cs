using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Reservation
{
    public class PostReservationDto
    {
        [Required]
        public int GuestCount { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Fullname { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        public string ReservationTime { get; set; }
    }
}
