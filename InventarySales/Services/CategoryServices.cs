using InventarySales.Common;
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

        public async Task<PagedResult<Category>> GetAllCategoriesAsync(int Page, int PageSize)
        {
            var count = await _context.Categories.CountAsync();
            var result = await _context.Categories.AsNoTracking().OrderBy(c => c.Id).Skip((Page - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync();
            return new PagedResult<Category>
            {
                Items = result,
                TotalCount = count,
                Page = Page,
                PageSize = PageSize
        
            }; ;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task AddCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }
     
    }
}
