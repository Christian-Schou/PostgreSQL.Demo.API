namespace PostgreSQL.Demo.API.Entities
{
    public class Author : Base
    {
        public string? Name { get; set; }
        public Gender? Gender { get; set; }
        public IEnumerable<Book>? Books { get; set; }
    }
}
