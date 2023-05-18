namespace MovieMania.Models
{
    public class Like
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string UserId { get; set; }
        public bool IsLiked { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}
