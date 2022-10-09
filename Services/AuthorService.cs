using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostgreSQL.Demo.API.Data;
using PostgreSQL.Demo.API.Entities;
using PostgreSQL.Demo.API.Helpers;
using PostgreSQL.Demo.API.Models.Authors;

namespace PostgreSQL.Demo.API.Services
{
    public interface IAuthorService
    {
        /// <summary>
        /// Get all authors in database. Set includeBooks to true if you want to include all books made by the author.
        /// </summary>
        /// <param name="includeBooks">Optional parameter to include books</param>
        /// <returns>All authors in database</returns>
        Task<IEnumerable<Author>> GetAllAuthorsAsync(bool includeBooks = false);

        /// <summary>
        /// Get a single author by Id and include books if requested by the includeBooks boolean.
        /// </summary>
        /// <param name="id">Id of Author</param>
        /// <param name="includeBooks">Optional parameter to include books</param>
        /// <returns>A single author</returns>
        Task<Author> GetAuthorByIdAsync(int id, bool includeBooks = false);

        /// <summary>
        /// Create a new author in the database
        /// </summary>
        /// <param name="model">Create Author request model</param>
        Task<int> CreateAuthor(CreateAuthorRequest model);

        /// <summary>
        /// Update an author in the database if the author already exists.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        Task UpdateAuthor(int id, UpdateAuthorRequest model);

        /// <summary>
        /// Delete a single author in the dabase. Will delete the author if the author exists in the database.
        /// Cascading is enabled and will delete the authors books from the database at the same time. Use with caution.
        /// </summary>
        /// <param name="id">Id of the author to delete</param>
        Task DeleteAuthor(int id);
    }

    public class AuthorService : IAuthorService
    {
        private LibraryContext _dbContext;
        private readonly IMapper _mapper;

        public AuthorService(LibraryContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> CreateAuthor(CreateAuthorRequest model)
        {
            // Validate new author
            if (await _dbContext.Authors.AnyAsync(x => x.Name == model.Name))
                throw new RepositoryException($"An author with the name {model.Name} already exists.");

            // Map model to new author object
            Author author = _mapper.Map<Author>(model);

            // Save Author
            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);

            if (author != null)
            {
                return author.Id; // Author got created
            }

            return 0;
            
        }

        public async Task DeleteAuthor(int id)
        {
            Author? author = await _getAuthorById(id);

            _dbContext.Authors.Remove(author); // Delete the author and books (Cascading is enabled)
            await _dbContext.SaveChangesAsync().ConfigureAwait(true);
        }

        public async Task<IEnumerable<Author>> GetAllAuthorsAsync(bool includeBooks = false)
        {
            if (includeBooks)
            {
                // Get all authors and their books
                return await _dbContext.Authors
                    .Include(b => b.Books)
                    .ToListAsync().ConfigureAwait(true);
            }
            else
            {
                // Get all authors without including the books
                return await _dbContext.Authors
                    .ToListAsync().ConfigureAwait(true);
            }
        }

        public async Task<Author> GetAuthorByIdAsync(int id, bool includeBooks = false)
        {
            return await _getAuthorById(id, includeBooks).ConfigureAwait(true);
        }

        public async Task UpdateAuthor(int id, UpdateAuthorRequest model)
        {
            Author? author = await _getAuthorById(id).ConfigureAwait(true);

            // Validation
            if (model.Name != author.Name && await _dbContext.Authors.AnyAsync(x => x.Name == model.Name))
                throw new RepositoryException($"An author with the name {model.Name} already exists.");

            // copy model to author and save
            _mapper.Map(model, author);
            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

        }

        /// <summary>
        /// Get a single author and the books if requested. Looks in the database for an author and returns null, if the author did not exist.
        /// </summary>
        /// <param name="id">Author ID</param>
        /// <param name="includeBooks">True to include books</param>
        /// <returns>A single author</returns>
        private async Task<Author> _getAuthorById(int id, bool includeBooks = false)
        {
            if (includeBooks)
            {
                Author? author = await _dbContext.Authors
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .Include(b => b.Books)
                    .FirstOrDefaultAsync().ConfigureAwait(true);

                if (author == null)
                {
                    throw new KeyNotFoundException("Author not found");
                }

                return author;
            }
            else
            {
                Author? author = await _dbContext.Authors
                    .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync().ConfigureAwait(true);

                if (author == null)
                {
                    throw new KeyNotFoundException("Author not found");
                }

                return author;
            }
        }
    }
}
