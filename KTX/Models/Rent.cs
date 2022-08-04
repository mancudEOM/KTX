namespace KTX.Models

{
    public class Rent
    {
        public int Id { get; set; }
        public string? Semeter { get; set; }
        public DateTime StartDateRent { get; set; }
        public DateTime DueDateRent { get; set; }

    }
}