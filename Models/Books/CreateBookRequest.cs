namespace PostgreSQL.Demo.API.Models.Books
{
    public class CreateBookRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public long ISBN13 { get; set; }
        public int AuthorId { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
