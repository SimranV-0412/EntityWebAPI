using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebAPI_Project.Models;
using static WebAPI_Project.DAL.MyAppDBContext;

namespace WebAPI_Project.DAL
{
    public class MyAppDBContext: IdentityDbContext<ApplicationUser>
    {
        public MyAppDBContext()
        {

        }
        public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options) 
        {
        }
        public virtual DbSet<Product> Product { get; set; }

        public List<Product> GetAllProducts()
        {
            return Product.FromSqlRaw("EXEC dbo.GetProduct").ToList();
        }
        public class ApplicationUser : IdentityUser
        {
            // Additional user properties
        }
    }
    //public class MyAppDBContext:DbContext
    //{
    //    public MyAppDBContext()
    //    {

    //    }
    //    public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options) 
    //    {
    //    }
    //    public virtual DbSet<Product> Product { get; set; }

    //    public List<Product> GetAllProducts()
    //    {
    //        return Product.FromSqlRaw("EXEC dbo.GetProduct").ToList();
    //    }
    //}

    //public class MyAppDBContext : IdentityDbContext<ApplicationUser>
    //{
    //    public MyAppDBContext(DbContextOptions<MyAppDBContext> options) : base(options)
    //    {
    //    }

    //    public DbSet<Product> Products { get; set; }

    //    // Optionally, add additional DbSets or customize as needed
    //}
}
