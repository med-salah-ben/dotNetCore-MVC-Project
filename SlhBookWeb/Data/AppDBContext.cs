using Microsoft.EntityFrameworkCore;
using SlhBookWeb.Models;

namespace SlhBookWeb.Data
{
    public class AppDBContext :DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
    }
}
