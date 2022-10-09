using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PostgreSQL.Demo.API.Entities;
using PostgreSQL.Demo.API.Models.Authors;
using PostgreSQL.Demo.API.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PostgreSQL.Demo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private IAuthorService _authorService;
        private IMapper _mapper;

        public AuthorController(IAuthorService authorService, IMapper mapper)
        {
            _authorService = authorService;
            _mapper = mapper;
        }

        // GET: api/<AuthorController>
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            IEnumerable<Author> authors = await _authorService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // GET api/<AuthorController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id, bool includeBooks = false)
        {
            Author author = await _authorService.GetAuthorByIdAsync(id, includeBooks);
            return Ok(author);
        }

        // POST api/<AuthorController>
        [HttpPost]
        public async Task<IActionResult> CreateAuthor(CreateAuthorRequest model)
        {
            int authorId = await _authorService.CreateAuthor(model);

            if (authorId != 0)
            {
                return Ok(new { message = $"Author was successfully created in database with the id {authorId}" });
            }

            return StatusCode(StatusCodes.Status500InternalServerError, "The author was not created in the database.");
        }

        // PUT api/<AuthorController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, UpdateAuthorRequest model)
        {
            await _authorService.UpdateAuthor(id, model);
            return Ok(new { message = "Author was successfully updated in database" });
            
        }

        // DELETE api/<AuthorController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            await _authorService.DeleteAuthor(id);
            return Ok(new { message = "Author was successfully deleted in database" });

        }
    }
}
