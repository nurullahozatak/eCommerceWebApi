
using ArtTrade.Models;
using Microsoft.EntityFrameworkCore;

namespace ArtTrade.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<AccountInfo> AccountInfos { get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<Category> Categories { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Customer ve Order arasında bir ilişki kurma örneği
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product ve Category arasında bir ilişki kurma örneği
            modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products) // Corrected property name from Product to Products
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

            // Cart ve Product arasında bir ilişki kurma örneği
            modelBuilder.Entity<Cart>()
                .HasOne(c => c.Product)
                .WithMany(p => p.Carts)
                .HasForeignKey(c => c.ProductId)
                .OnDelete(DeleteBehavior.Cascade);


            // Coupon ve Customer arasında bir ilişki kurma örneği
            modelBuilder.Entity<Coupon>()
            .HasOne(c => c.Customer)
            .WithMany(c => c.Coupons)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.SetNull);


            // Comment ve Product arasında bir ilişki kurma örneği
            modelBuilder.Entity<Comment>()
            .HasOne(c => c.Product)
            .WithMany(p => p.Comments)
            .HasForeignKey(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade);


            // AccountInfo ve Customer arasında bir ilişki kurma örneği
            modelBuilder.Entity<AccountInfo>()
            .HasOne(ai => ai.Customer)
            .WithOne(c => c.AccountInfo)
            .HasForeignKey<AccountInfo>(ai => ai.CustomerId) // Adjust the foreign key property
            .OnDelete(DeleteBehavior.Cascade);


            // Product ve Seller arasında bir ilişki kurma örneği
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Seller)
                .WithMany(s => s.Products)
                .HasForeignKey(p => p.SellerId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
