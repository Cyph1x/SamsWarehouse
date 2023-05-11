using Microsoft.EntityFrameworkCore;

namespace SamsWarehouse.Models.Data
{
    public class SQLDBContext : DbContext
    {
        public SQLDBContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Product)
                .WithMany(j => j.CartItems)
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<CartItem>()
                .HasOne(ci => ci.Cart)
                .WithMany(fl => fl.CartItems)
                .HasForeignKey(c => c.CartId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
