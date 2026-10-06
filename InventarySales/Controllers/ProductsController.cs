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
        new Product { Id = 1, Name = "Product 1", Price = 10.99, Stock = 10 },
        new Product { Id = 2, Name = "Product 2", Price = 19.99, Stock = 5 },
        new Product { Id = 3, Name = "Product 3", Price = 5.99, Stock = 20 }
};
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return Products;
        }
    };
}
