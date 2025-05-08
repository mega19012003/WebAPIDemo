namespace WebAPI.Models
{
    public class Table
    {
        public Guid TableId { get; set; } //= Guid.NewGuid().ToString();
        public string TableName { get; set; }
        public int Seats { get; set; }
    }
}
