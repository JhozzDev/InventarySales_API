using InventarySales.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventarySales.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public static readonly List<Product> Products = new List<Product>
    {
        new Product { Id = 1, Name = "Product 1", Price = 10.99m, Stock = 10 },
        new Product { Id = 2, Name = "Product 2", Price = 19.99m, Stock = 5 },
        new Product { Id = 3, Name = "Product 3", Price = 5.99m, Stock = 20 }
};
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Products;
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
           
            var product = Products.FirstOrDefault(product => product.Id == id);
            if (product is null)
                return NotFound();
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            product.Id = Products.Max(p => p.Id) + 1;
            Products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = Products.FirstOrDefault(product => product.Id == id);
            if (product is null)
                return NotFound();
            Products.Remove(product);
            return NoContent();
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct is null)
                return NotFound();
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            existingProduct.Stock = product.Stock;
            return NoContent();
        }
    };

    
}
