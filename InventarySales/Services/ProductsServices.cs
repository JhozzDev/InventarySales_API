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

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _context.Products.Include(p => p.Category).ToListAsync();
        }
        public async Task<bool> CategoryExistsAsync(int categoryId)
        {
            return await _context.Categories.AnyAsync(c => c.Id == categoryId);
        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddProductAsync(Product product)
        {
                  _context.Products.Add(product);
                              await _context.SaveChangesAsync();


        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product is not null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProductAsync(int id, Product updatedProduct)
        {
            var existingProduct = await GetProductByIdAsync(id);
            if (existingProduct is not null)
            {
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Stock = updatedProduct.Stock;
                existingProduct.CategoryId = updatedProduct.CategoryId;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int category)
        {
            return await _context.Products.Include(p => p.Category).Where(p => p.CategoryId == category).ToListAsync();
        }

     
    }
}
