namespace WebSeeSharpers.Models
{
    public class LostItem
    {
        public int Id { get; set; } 
        public string Item { get; set; }

        public string? Description { get; set; }

        public DateTime TimeFound { get; set; }

        public string? Picture { get; set; }

    }
}
