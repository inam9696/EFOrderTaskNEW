using EFOrderTask.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection;

namespace EFOrderTask.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<ItemUnit> UnitItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderedItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>()
                         .HasKey(k => new { k.OrderId_FK, k.ItemId_Fk, k.UnitId_Fk });
            modelBuilder.Entity<OrderItem>()
                        .HasOne(o => o.Order)
                        .WithMany(oi => oi.OrderedItems)
                        .HasForeignKey(f => f.OrderId_FK)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                       .HasOne(i => i.Item)
                       .WithMany(oi => oi.OrderedItems)
                       .HasForeignKey(f => f.ItemId_Fk)
                       .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                       .HasOne(u => u.Unit)
                       .WithMany(oi => oi.OrderedItems)
                       .HasForeignKey(f => f.UnitId_Fk)
                       .OnDelete(DeleteBehavior.Cascade);

            //Primary Keys Defined
            modelBuilder.Entity<Item>()
                       .HasKey(x => x.Item_Id);

            modelBuilder.Entity<Unit>()
                        .HasKey(x => x.Unit_Id);

            modelBuilder.Entity<Order>()
                        .HasKey(x => x.Order_Id);


            modelBuilder.Entity<OrderItem>()
                        .Property(c => c.Customer_Name)
                        .IsRequired(true);

            //modelBuilder.Entity<OrderedItem>()
            //            .Property(q => q.Quantity)
            //            .IsRequired(true);

            //modelBuilder.Entity<OrderedItem>()
            //            .Property(s => s.Sub_Total)
            //            .IsRequired(true);

            //Item 
            modelBuilder.Entity<Item>()
                        .Property(e => e.Item_Name)
                        .IsRequired(true)
                        .HasMaxLength(50);

           


            //Order
            //modelBuilder.Entity<Order>()
            //            .Property(e => e.Date)
            //            .IsRequired(true);

            modelBuilder.Entity<Order>()
                        .Property(e => e.Total_Price)
                        .IsRequired(true);

            // Many to Many relationship 
            modelBuilder.Entity<ItemUnit>()
                      .HasKey(k => new { k.UnitId_FK, k.ItemId_FK });

            modelBuilder.Entity<ItemUnit>()
                        .HasOne(u => u.Unit)
                        .WithMany(ui => ui.ItemUnits)
                        .HasForeignKey(f => f.UnitId_FK)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ItemUnit>()
                       .HasOne(i => i.Item)
                       .WithMany(ui => ui.ItemUnits)
                       .HasForeignKey(f => f.ItemId_FK)
                       .OnDelete(DeleteBehavior.Cascade);

           

            modelBuilder.Entity<ItemUnit>()
                       .Property(p => p.Price)
                       .IsRequired(true);
        }
    }
}
