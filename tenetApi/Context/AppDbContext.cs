﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using tenetApi.Model;

namespace tenetApi.Context
{
    public class AppDbContext : IdentityDbContext<User,Role , long>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }
        public DbSet<Customer> customers { get; set; }
        public DbSet<CustomerAddress> customerAddresses { get; set; }
        public DbSet<Shop> shops { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Promotion> promotion { get; set; }
        public DbSet<ProductCategory> productCategories { get; set; }
        public DbSet<ShopCategory> shopCategories { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Customer>().HasOne(b => b.userFk).WithOne(c => c.customerFk).HasForeignKey("Customer").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Customer>().HasMany(b => b.custAdresFk).WithOne(c => c.customerFk).HasForeignKey("CustomerID").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Shop>().HasOne(b => b.userFk).WithOne(c => c.shopFk).HasForeignKey("Shop").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().HasOne(b => b.shopFk).WithMany(c => c.productFk).HasForeignKey("ShopID").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Product>().HasMany(b => b.promotionFk).WithOne(c => c.productFk).HasForeignKey("ProductID").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<ShopCategory>().HasMany(b => b.shopFk).WithOne(c => c.shopCategoryFk).HasForeignKey("ShopCategoryID").OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Customer>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<CustomerAddress>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<Product>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<ProductCategory>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<Promotion>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<User>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<Shop>().HasQueryFilter(p => p.IsDeleted == false);
            builder.Entity<ShopCategory>().HasQueryFilter(p => p.IsDeleted == false);
        }
    }
}
