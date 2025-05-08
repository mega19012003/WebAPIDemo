using System.Text.Json.Serialization;

namespace WebAPI.Models
{
    public class Reservation
    {
        public Guid ReservationId { get; set; } //= Guid.NewGuid().ToString();
        public bool isConfirmed { get; set; } = false;
        public int GuestCount { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fullname { get; set; }
        public Guid? TableId { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservationTime { get; set; }
        public bool IsCancelled { get; set; } = false;

        public Table? Table { get; set; }
    }
}
