using System.ComponentModel.DataAnnotations;

namespace WebAPI.Dtos.Reservation
{
    public class PutReservationDto
    {
        public Guid ReservationId { get; set; }
        [Required]
        public bool IsConfirmed { get; set; }
        public Guid TableId { get; set; }
        [Required]
        public DateTime ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        [Required]
        public int GuestCount { get; set; }
    }
}
