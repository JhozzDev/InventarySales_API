using InventarySales.Common;
using InventarySales.Dtos;
using InventarySales.Models;
using InventarySales.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventarySales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryServices _services;

        public CategoriesController(CategoryServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<CategoryDto>>> Get([FromQuery] PaginationParams Params)
        {
            
            var categories = await _services.GetAllCategoriesAsync(Params);
          
            var result = categories.Items.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            });
            return Ok(new PagedResult<CategoryDto>
            {
                Items = result.ToList(),
                TotalCount = categories.TotalCount,
                Page = categories.Page,
                PageSize = categories.PageSize
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id)
        {
            var category = await _services.GetCategoryByIdAsync(id);
            if (category is null)
                return NotFound();

            return Ok(new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }

        [HttpPost]
        public async Task<ActionResult> AddCategory([FromBody] CategoryCreateDto categoryDto)
        {
            var category = new Category
            {
                Name = categoryDto.Name
            };

            await _services.AddCategoryAsync(category);
            return CreatedAtAction(nameof(GetById), new { id = category.Id }, new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            });
        }
    }
}
