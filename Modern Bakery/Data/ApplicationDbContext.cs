using BakeryShop.Models;
using Microsoft.EntityFrameworkCore;

namespace Modern_Bakery.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        public DbSet<ContactMessage> ContactMessages { get; set; }
        public DbSet<Product> Products { get; set; }
    }
}
