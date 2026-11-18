using InventarySales.DataBase;
using InventarySales.Models;
using Microsoft.EntityFrameworkCore;
namespace InventarySales.Services
{
    public class CategoryServices
    {
        private DbContextApp _context;

        public CategoryServices(DbContextApp context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Include(c => c.Products).ToListAsync();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.Include(c => c.Products).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
     
    }
}
