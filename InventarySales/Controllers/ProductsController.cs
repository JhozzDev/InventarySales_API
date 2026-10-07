using InventarySales.Models;
using Microsoft.AspNetCore.Mvc;
using InventarySales.Services;
namespace InventarySales.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {

        private readonly ProductsServices _services;

        public ProductsController(ProductsServices services)
        {
            _services = services;
        }

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _services.GetAllProducts();
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = _services.GetProductById(id);
            if (product is null)
                return NotFound();
            return Ok(product);
        }
        [HttpPost]
        public ActionResult AddProduct([FromBody] Product product)
        {
            _services.AddProduct(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteProduct(int id)
        {
            _services.DeleteProduct(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            _services.UpdateProduct(id, updatedProduct);
            return NoContent();
        }
    }
}
