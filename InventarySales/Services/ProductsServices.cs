using InventarySales.Common;
using InventarySales.DataBase;
using InventarySales.Dtos;
using InventarySales.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace InventarySales.Services
{
    public class ProductsServices
    {
        private readonly DbContextApp _context;
        private static readonly Dictionary<string, Expression<Func<Product, object>>> SortMap = new()
        {
            ["id"] = p => p.Id,
            ["name"] = p => p.Name,
            ["price"] = p => p.Price,
            ["stock"] = p => p.Stock
        };

        public ProductsServices(DbContextApp context)
        {
            _context = context;
        }

        public async Task<PagedResult<Product>> GetAllProductsAsync(PaginationParams Params, decimal? MaxPrice, decimal? MinPrice, int? CategoryId, string? SearchTerm, string? sortBy)
        {


            IQueryable<Product> query = _context.Products.Where(p => (!MaxPrice.HasValue || p.Price <= MaxPrice) &&
                                                              (!MinPrice.HasValue || p.Price >= MinPrice) &&
                                                              (!CategoryId.HasValue || p.CategoryId == CategoryId) &&
                                                              (string.IsNullOrEmpty(SearchTerm) || p.Name.Contains(SearchTerm)));
                
            var products = await query 
                .Include(p => p.Category)
                .OrderBy(SortMap[sortBy])
                .AsNoTracking()
                .Skip((Params.Page - 1) * Params.PageSize)
                .Take(Params.PageSize)
                .ToListAsync();
            
            var count = await query.CountAsync();

            return new PagedResult<Product>
            {
                Items = products,
                TotalCount = count,
                Page = Params.Page,
                PageSize = Params.PageSize
            };
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

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int category, PaginationParams Params)
        {
            return await _context.Products.Include(p => p.Category).Where(p => p.CategoryId == category).OrderBy(p => p.Id)
                .Skip((Params.Page - 1) * Params.PageSize)
                .Take(Params.PageSize)
                .ToListAsync();
        }

     
    }
}
