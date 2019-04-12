using APIPROJECT.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Models
{
    public class ShopDataDbContext : DbContext
    {
        public ShopDataDbContext()
        {
        }

        public ShopDataDbContext
            (DbContextOptions<ShopDataDbContext> options) : base(options)
        { }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Feedback> Feedbacks{ get; set; }
        public DbSet<Order> Orders { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Data Source=TRD-520; Initial Catalog=ShoppingProject;Integrated Security=true;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasOne(c => c.Vendor)
                .WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Feedback>()
           .HasOne(c => c.Customer)
           .WithMany(p => p.Feedbacks)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
           .HasOne(c => c.Customer)
           .WithMany(o => o.Orders)
           .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vendor>(entity =>
            {
                entity.Property(e => e.VendorName)
                .HasColumnName("VendorName")
                .HasMaxLength(15)
                .IsUnicode(false);
            });
            base.OnModelCreating(modelBuilder);
        }

    }
}