using Microsoft.EntityFrameworkCore;
using PostgreSQL.Demo.API.Entities;

namespace PostgreSQL.Demo.API.Data
{
    public class LibraryContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public LibraryContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("LibraryDatabase");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Author>()
                .HasMany(b => b.Books)
                .WithOne(a => a.Author)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
