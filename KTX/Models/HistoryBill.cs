namespace KTX.Models


{
    public class HistoryBill
    {
        public int Id { get; set; }
        public int RoomId { get; set; }

        public List<Bill>? Bills { get; set; }
        public Room? Room { get; set; }
    }
}