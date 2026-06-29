using BookStoreAPI.Models;
using BookStoreAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreAPI.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoriesController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAll();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cat = await _categoryService.GetById(id);

            if (cat == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            return Ok(cat);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create([FromBody] CreateCategoryDto dto)
        {
            if (string.IsNullOrEmpty(dto.Name))
            {
                return BadRequest(new { message = "Category name is required." });
            }

            var cat = await _categoryService.Create(dto);
            return CreatedAtAction(nameof(GetById), new { id = cat.Id }, cat);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id, [FromBody] CreateCategoryDto dto)
        {
            var cat = await _categoryService.Update(id, dto);

            if (cat == null)
            {
                return NotFound(new { message = "Category not found." });
            }

            return Ok(cat);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var deleted = await _categoryService.Delete(id);

                if (!deleted)
                {
                    return NotFound(new { message = "Category not found." });
                }

                return Ok(new { message = "Category deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
