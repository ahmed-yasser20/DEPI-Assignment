using BookStoreAPI.Models;
using BookStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly AuthorService _authorService;

        public AuthorsController(AuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var authors = await _authorService.GetAll();
            return Ok(authors);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var author = await _authorService.GetById(id);

            if (author == null)
            {
                return NotFound(new { message = "Author not found." });
            }

            return Ok(author);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateAuthorDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest(new { message = "Author name is required." });
            }

            var author = await _authorService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = author.Id }, author);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateAuthorDto dto)
        {
            var author = await _authorService.Update(id, dto);

            if (author == null)
            {
                return NotFound(new { message = "Author not found." });
            }

            return Ok(author);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _authorService.Delete(id);

                if (!deleted)
                {
                    return NotFound(new { message = "Author not found." });
                }

                return Ok(new { message = "Author deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
