using Microsoft.EntityFrameworkCore;
using ShoppingCart.Domain.Model.Entities;

namespace ShoppingCart.Data
{
    public  class CartDbContext : DbContext
    {
        public DbSet<Basket> Basket { get; set; }
        public DbSet<BasketItem> BasketItem { get; set; }
        public DbSet<Product> Product { get; set; }
        public CartDbContext(DbContextOptions options) : base(options)
        {
           
        }

    }
}
