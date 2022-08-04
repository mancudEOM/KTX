namespace KTX.Models


{
    public class HistoryRent
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }
        public List<Rent>? Rents { get; set; }
    }
}