namespace WebSeeSharpers.Models
{
    public class Rating
    {
        public int Id { get; set; }
        public string? ViewerName { get; set; }
        
        public string Comment { get; set; }

        public int MovieId { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
