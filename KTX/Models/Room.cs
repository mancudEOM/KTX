namespace KTX.Models

{
    public class Room
    {
        public int Id { get; set; }
        public string? Block { get; set; }
        public string? RoomCode { get; set; }
        public int Type { get; set; }
        public List<User>? Users { get; set; }
        public HistoryBill? HistoryBill { get; set; }

    }
}