namespace KTX.Models

{
    public class Bill
    {
        public int Id { get; set; }
        public float ElectricityIndexS { get; set; }
        public float ElectricityIndexE { get; set; }
        public float WaterIndexS { get; set; }
        public float WaterIndexE { get; set; }
        public float Total { get; set; }
        public int Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime DueDate { get; set; }


    }
}