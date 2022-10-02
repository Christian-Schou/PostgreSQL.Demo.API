namespace PostgreSQL.Demo.API.Entities
{
    public class Book : Base
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Publisher { get; set; }
        public long ISBN13 { get; set; }
        public Author? Author { get; set; }
        public int AuthorId { get; set; }
    }
}
