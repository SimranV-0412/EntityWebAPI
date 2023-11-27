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
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
        public virtual DbSet<Product> Product { get; set; }

        public List<Product> GetAllProducts()
        {
            return Product.FromSqlRaw("EXEC dbo.GetProduct").ToList();
        }
    }
}
