using Microsoft.EntityFrameworkCore;
using InventarySales.Models;
namespace InventarySales.DataBase
{
    public class DbContextApp : DbContext
    {
        public DbContextApp(DbContextOptions<DbContextApp> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = null!;
        
    }
};
