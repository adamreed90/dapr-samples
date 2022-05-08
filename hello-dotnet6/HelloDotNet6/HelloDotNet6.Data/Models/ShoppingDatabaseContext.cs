using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelloDotNet6.Data.Models.DaprShopping;
using Microsoft.EntityFrameworkCore;

namespace HelloDotNet6.Data.Models
{
    public class ShoppingDatabaseContext : DbContext
    {

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Order> Orders { get; set; }

        public ShoppingDatabaseContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;userid=root;password=my-secret-pw;database=DaprShopping;", new MariaDbServerVersion(new Version(10, 7, 3)))
            // The following three options help with debugging, but should
            // be changed or removed for production.
            //.LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors();
        }
        public ShoppingDatabaseContext(DbContextOptions<ShoppingDatabaseContext> options)
        : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("DaprShopping");

            modelBuilder.Entity<Customer>(o =>
            {
                o.ToTable("Customers");
                o.HasKey(k => k.CustomerId);
                o.Property(p => p.LastModifiedTimestamp).IsConcurrencyToken();
            });

            modelBuilder.Entity<Product>(o =>
            {
                o.ToTable("Products");
                o.HasKey(k => k.ProductId);
                o.HasIndex(i => i.Name);
                o.Property(p => p.LastModifiedTimestamp).IsConcurrencyToken();
            });

            modelBuilder.Entity<Inventory>(o =>
            {
                o.ToTable("Inventory");
                o.HasKey(k => k.InventoryId);
                o.HasOne(h => h.Product);
                o.Property(p => p.LastModifiedTimestamp).IsConcurrencyToken();
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.ToTable("Orders");
                o.HasKey(k => k.OrderId);
                o.HasMany(m => m.OrderItems).WithOne(o => o.Order);
                o.Property(p => p.LastModifiedTimestamp).IsConcurrencyToken();
            });

            base.OnModelCreating(modelBuilder);
        }

        #region SaveChanges Overrides
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                .Where(t => t.Entity is DaprShoppingBaseClass && t.State == EntityState.Added)
                .Select(t => t.Entity)
                .ToArray();

            var modified = ChangeTracker.Entries()
                .Where(t => t.Entity is DaprShoppingBaseClass && t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (DaprShoppingBaseClass entity in added)
            {
                entity.CreatedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                entity.LastModifiedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            foreach (DaprShoppingBaseClass entity in modified)
            {
                entity.LastModifiedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            var added = ChangeTracker.Entries()
                .Where(t => t.Entity is DaprShoppingBaseClass && t.State == EntityState.Added)
                .Select(t => t.Entity)
                .ToArray();

            var modified = ChangeTracker.Entries()
                .Where(t => t.Entity is DaprShoppingBaseClass && t.State == EntityState.Modified)
                .Select(t => t.Entity)
                .ToArray();

            foreach (DaprShoppingBaseClass entity in added)
            {
                entity.CreatedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                entity.LastModifiedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            foreach (DaprShoppingBaseClass entity in modified)
            {
                entity.LastModifiedTimestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            }

            return base.SaveChanges();
        }
        #endregion
    }
}
