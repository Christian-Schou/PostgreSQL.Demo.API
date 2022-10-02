namespace PostgreSQL.Demo.API.Models.Books
{
    public class UpdateBookRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public long ISBN13 { get; set; }
        public int AuthorId { get; set; }
        public DateTime Updated { get; set; } = DateTime.Now;
    }
}
