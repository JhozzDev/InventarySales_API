using InventarySales.DataBase;
using InventarySales.Models;
using Microsoft.EntityFrameworkCore;

namespace InventarySales.Services
{
    public class ProductsServices
    {

        private readonly DbContextApp _context;
        public ProductsServices(DbContextApp context)
        {
            _context = context;
        }

        public IEnumerable<Product> GetAllProducts() => _context.Products.ToList();
        public Product? GetProductById(int id) => _context.Products.FirstOrDefault(p => p.Id == id);

        public void AddProduct(Product product)
        {
            
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProductById(id);
            if (product is not null)
                _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(int id, Product updatedProduct)
        {
            var existingProduct = GetProductById(id);
            if (existingProduct is not null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Stock = updatedProduct.Stock;
                _context.SaveChanges();
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _context.Products.Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase));
        }

    }
}
