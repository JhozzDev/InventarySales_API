using InventarySales.Common;
using InventarySales.DataBase;
using InventarySales.Dtos;
using InventarySales.Models;
using Microsoft.EntityFrameworkCore;
namespace InventarySales.Services
{
    public class CategoryServices
    {
        private readonly DbContextApp _context;

        public CategoryServices(DbContextApp context)
        {
            _context = context;
        }

        public async Task<PagedResult<Category>> GetAllCategoriesAsync(PaginationParams Params)
        {
            IQueryable<Category> query = _context.Categories.AsQueryable();
            var count = await query.CountAsync();
            var result = await query.AsNoTracking().OrderBy(c => c.Id).Skip((Params.Page - 1) * Params.PageSize)
                .Take(Params.PageSize)
                .ToListAsync();
            return new PagedResult<Category>
            {
                Items = result,
                TotalCount = count,
                Page = Params.Page,
                PageSize = Params.PageSize
        
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
