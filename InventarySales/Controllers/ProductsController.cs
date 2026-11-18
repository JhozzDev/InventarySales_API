using InventarySales.Models;
using InventarySales.Services;
using Microsoft.AspNetCore.Mvc;
using InventarySales.Dtos;
namespace InventarySales.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ProductsServices _services;

        public ProductsController(ProductsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await _services.GetAllProductsAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            var product = await _services.GetProductByIdAsync(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }

        [HttpGet("category/{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategory(int category)
        {
            var products = await _services.GetProductsByCategoryAsync(category);
            return Ok(products);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] ProductCreateDto product)
        {
  
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
                CategoryId = product2.CategoryId    


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
    }
}
