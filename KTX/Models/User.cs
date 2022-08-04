namespace KTX.Models

{
    public class User
    {
        public int Id { get; set; }
        public string? IdentityId { get; set; }
        public DateTime? BirthDay { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? ImageUrl { get; set; }        
        public string? Gender { get; set; }
        public string? TelephoneNumber { get; set; }
        public string? Email { get; set; }
        public List<RelativeUser>? RelativeUsers { get; set; }
        public HistoryRent? HistoryRent { get; set; }

    }
}
