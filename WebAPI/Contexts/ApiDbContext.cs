using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Contexts
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext() { }
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }


        public DbSet<User> Users { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(x => x.Status)
            .HasConversion<string>();

            modelBuilder.Entity<User>().Property(x => x.Role)
            .HasConversion<string>();

            modelBuilder.Entity<Product>().Property(x => x.Price).HasConversion<decimal>();

            modelBuilder.Entity<User>().HasMany(x => x.Orders).WithOne(x => x.Customer)
            .HasForeignKey(x => x.CustomerEmail)
            .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>().HasMany(x => x.OrderDetails).WithOne(x => x.Order)
            .HasForeignKey(x => x.OrderId)
            .OnDelete(DeleteBehavior.Cascade);

            // Configure unique index for Email property
            /*modelBuilder.Entity<Address>()
                .HasIndex(a => a.Email)
                .IsUnique();*/

            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(a => a.Email);
                entity.HasOne(a => a.Customer)
                      .WithOne(u => u.Address)
                      .HasForeignKey<Address>(a => a.Email)
                      .IsRequired();
            });

        }
    }
}
