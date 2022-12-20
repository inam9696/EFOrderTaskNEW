﻿// <auto-generated />
using System;
using EFOrderTask.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EFOrderTask.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20221219160937_initAll2")]
    partial class initAll2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EFOrderTask.Models.Item", b =>
                {
                    b.Property<int>("Item_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Item_Id"), 1L, 1);

                    b.Property<string>("Item_Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Item_Id");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("EFOrderTask.Models.ItemUnit", b =>
                {
                    b.Property<int>("UnitId_FK")
                        .HasColumnType("int");

                    b.Property<int>("ItemId_FK")
                        .HasColumnType("int");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("Quatity")
                        .HasColumnType("int");

                    b.HasKey("UnitId_FK", "ItemId_FK");

                    b.HasIndex("ItemId_FK");

                    b.ToTable("UnitItems");
                });

            modelBuilder.Entity("EFOrderTask.Models.Order", b =>
                {
                    b.Property<int>("Order_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Order_Id"), 1L, 1);

                    b.Property<string>("CustomerGuidKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("Total_Price")
                        .IsRequired()
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Order_Id");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("EFOrderTask.Models.OrderItem", b =>
                {
                    b.Property<int?>("OrderId_FK")
                        .HasColumnType("int");

                    b.Property<int?>("ItemId_Fk")
                        .HasColumnType("int");

                    b.Property<int?>("UnitId_Fk")
                        .HasColumnType("int");

                    b.Property<string>("CustomerGuidKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Customer_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Order_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal?>("Sub_Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId_FK", "ItemId_Fk", "UnitId_Fk");

                    b.HasIndex("ItemId_Fk");

                    b.HasIndex("UnitId_Fk");

                    b.ToTable("OrderedItems");
                });

            modelBuilder.Entity("EFOrderTask.Models.Unit", b =>
                {
                    b.Property<int>("Unit_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Unit_Id"), 1L, 1);

                    b.Property<string>("Unit_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Unit_Id");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("EFOrderTask.Models.ItemUnit", b =>
                {
                    b.HasOne("EFOrderTask.Models.Item", "Item")
                        .WithMany("ItemUnits")
                        .HasForeignKey("ItemId_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFOrderTask.Models.Unit", "Unit")
                        .WithMany("ItemUnits")
                        .HasForeignKey("UnitId_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("EFOrderTask.Models.OrderItem", b =>
                {
                    b.HasOne("EFOrderTask.Models.Item", "Item")
                        .WithMany("OrderedItems")
                        .HasForeignKey("ItemId_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFOrderTask.Models.Order", "Order")
                        .WithMany("OrderedItems")
                        .HasForeignKey("OrderId_FK")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EFOrderTask.Models.Unit", "Unit")
                        .WithMany("OrderedItems")
                        .HasForeignKey("UnitId_Fk")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Order");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("EFOrderTask.Models.Item", b =>
                {
                    b.Navigation("ItemUnits");

                    b.Navigation("OrderedItems");
                });

            modelBuilder.Entity("EFOrderTask.Models.Order", b =>
                {
                    b.Navigation("OrderedItems");
                });

            modelBuilder.Entity("EFOrderTask.Models.Unit", b =>
                {
                    b.Navigation("ItemUnits");

                    b.Navigation("OrderedItems");
                });
#pragma warning restore 612, 618
        }
    }
}
