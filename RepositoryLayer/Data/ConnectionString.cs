using DomainLayer.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Data
{
    public class ConnectionString : DbContext
    {
        public ConnectionString(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }
        public DbSet<SupplierItem> SupplierItems { get; set; }
        public DbSet<CustomerItem> CustomerItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemImage> ItemImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
               .HasOne(e => e.UserType)
               .WithMany(e => e.Users)
               .HasForeignKey(e => e.UserTypeId)
               .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasOne(e => e.Category)
                .WithMany(e => e.Items)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerItem>()
                .HasOne(e => e.Item)
                .WithOne(e => e.CustomerItem)
                .HasForeignKey<CustomerItem>(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierItem>()
                .HasOne(e => e.Item)
                .WithOne(e => e.SupplierItem)
                .HasForeignKey<SupplierItem>(e => e.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Item>()
                .HasMany(e => e.ItemImages)
                .WithOne(e => e.Item)
                .HasForeignKey(e => e.ItemImageId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CustomerItem>()
                    .HasOne(e => e.User)
                    .WithMany(e => e.CustomerItems)
                    .HasForeignKey(e => e.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SupplierItem>()
                  .HasOne(e => e.User)
                  .WithMany(e => e.SupplierItems)
                  .HasForeignKey(e => e.UserId)
                  .OnDelete(DeleteBehavior.Restrict);
        }

       

    }
}
