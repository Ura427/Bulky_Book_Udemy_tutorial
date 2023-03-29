using Bulky_Book_tutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace Bulky_Book_tutorial.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Category> Categories { get; set; }
    }
}
