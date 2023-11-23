using Microsoft.EntityFrameworkCore;
using WebAPI_Project.Models;

namespace WebAPI_Project.DAL
{
    public class MyAppDBContext:DbContext
    {
        public MyAppDBContext()
        {
                
        }
        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options) 
        {
        }
        public virtual DbSet<Product> Products { get; set; }
    }

    //public class MyAppDBContext : IdentityDbContext<ApplicationUser>
    //{
    //    public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options)
    //    {
    //    }

    //    public DbSet<Product> Products { get; set; }

    //    // Optionally, add additional DbSets or customize as needed
    //}
}
