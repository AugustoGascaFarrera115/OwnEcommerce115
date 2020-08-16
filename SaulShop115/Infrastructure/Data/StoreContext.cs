using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class StoreContext : DbContext
    {
        public StoreContext(DbContextOptions<StoreContext> options) : base(options)
        {

        }



        //Products will be the products table
        //DbSet<Product> take the entiti named product
        public DbSet<Product> Products { get; set; }
    }
}