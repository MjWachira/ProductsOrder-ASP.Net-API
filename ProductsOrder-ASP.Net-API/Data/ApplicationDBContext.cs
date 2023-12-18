using Microsoft.EntityFrameworkCore;
using ProductsOrder_ASP.Net_API.Models;

namespace ProductsOrder_ASP.Net_API.Data
{
    public class ApplicationDBContext:DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext>options):base(options) 
        {
            
        }
        public DbSet<Products>Products { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<Users> Users { get; set; }
    }
}
