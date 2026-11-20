using InventarySales.Common;
using InventarySales.Dtos;
using InventarySales.Models;
using InventarySales.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
namespace InventarySales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {

        private static readonly string[] AllowedSorts = ["id", "name", "price", "stock"];
        private readonly ProductsServices _services;

        public ProductsController(ProductsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<ProductDto>>> Get([FromQuery] PaginationParams Params, [FromQuery] decimal? maxPrice, [FromQuery] decimal? minPrice, [FromQuery] int? CategoryId, [FromQuery] string? SearchTerm, [FromQuery] string? sortBy = "id", [FromQuery] bool? ascending = true)
        {

         if (!string.IsNullOrWhiteSpace(sortBy) &&
        !AllowedSorts.Contains(sortBy.ToLowerInvariant()))
                return BadRequest(new { message = $"sortBy no válido: {sortBy}" });

            var products = await _services.GetAllProductsAsync(Params, maxPrice, minPrice, CategoryId, SearchTerm, sortBy, ascending);
            
            var result = products.Items.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            });
            return Ok(new PagedResult<ProductDto>
            {
                Items = result.ToList(),
                TotalCount = products.TotalCount,
                Page = products.Page,
                PageSize = products.PageSize
            });
        }

 
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetById(int id)
        {
            var product = await _services.GetProductByIdAsync(id);
            if (product is null)
                return NotFound();
            return Ok(new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name
            });
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProductsByCategory(int category, [FromQuery] PaginationParams Params)
        {
            var products = await _services.GetProductsByCategoryAsync(category, Params);
            return Ok(products.Select(p => new ProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                CategoryName = p.Category?.Name
            }));
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ProductCreateDto product)
        {
            if (!await _services.CategoryExistsAsync(product.CategoryId))
                return UnprocessableEntity(new
                {
                    message = $"La categoría {product.CategoryId} no existe."
                });

            var product2 = new Product
            {
                Name = product.Name,
                Price = product.Price,
                CategoryId = product.CategoryId,
                Stock = product.Stock
            };
            await _services.AddProductAsync(product2);
            return CreatedAtAction(nameof(GetById), new { id = product2.Id }, new ProductDto
            {
                Id = product2.Id,
                Name = product2.Name,
                Price = product2.Price,
                Stock = product2.Stock,
                CategoryId = product2.CategoryId   ,
                CategoryName = product2.Category?.Name


            });
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            if(!await _services.DeleteProductAsync(id)) return NotFound();
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, [FromBody] ProductCreateDto updatedProduct)
        {
            if (!await _services.CategoryExistsAsync(updatedProduct.CategoryId))
                return UnprocessableEntity(new
                {
                    message = $"La categoría {updatedProduct.CategoryId} no existe."
                });

            var product = new Product
           {
               Id = id,
               Name = updatedProduct.Name,
               Price = updatedProduct.Price,
               CategoryId = updatedProduct.CategoryId,
               Stock = updatedProduct.Stock
           };
            if(!await _services.UpdateProductAsync(id, product)) return NotFound();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchProduct(int id, [FromBody] ProductPatchDto patch)
        {
            if (!await _services.PatchProductAsync(id, patch.Price, patch.Stock))
                return NotFound();

            return NoContent();
        }
    }
}
