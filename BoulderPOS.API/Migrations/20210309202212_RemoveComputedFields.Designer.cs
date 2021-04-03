﻿// <auto-generated />
using System;
using BoulderPOS.API.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace BoulderPOS.API.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20210309202212_RemoveComputedFields")]
    partial class RemoveComputedFields
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("BoulderPOS.API.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("date")
                        .HasColumnName("birthDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("firstName");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("lastName");

                    b.Property<bool>("NewsletterSubscription")
                        .HasColumnType("boolean")
                        .HasColumnName("newsletterSubscription");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("varchar(16)")
                        .HasColumnName("phoneNumber");

                    b.Property<string>("Picture")
                        .HasColumnType("varchar")
                        .HasColumnName("picturePath");

                    b.Property<byte[]>("PicturePreviewPath")
                        .HasColumnType("bytea")
                        .HasColumnName("picturePreviewPath");

                    b.HasKey("Id")
                        .HasName("pK_customers");

                    b.ToTable("customers");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.CustomerEntries", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customerId");

                    b.Property<int>("Quantity")
                        .HasColumnType("integer")
                        .HasColumnName("quantity");

                    b.Property<bool>("UnlimitedEntries")
                        .HasColumnType("boolean")
                        .HasColumnName("unlimitedEntries");

                    b.HasKey("Id")
                        .HasName("pK_customerEntries");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasDatabaseName("iX_customerEntries_customerId");

                    b.ToTable("customerEntries");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.CustomerSubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<bool>("AutoRenewal")
                        .HasColumnType("boolean")
                        .HasColumnName("autoRenewal");

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customerId");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date")
                        .HasColumnName("endDate");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date")
                        .HasColumnName("startDate");

                    b.HasKey("Id")
                        .HasName("pK_customerSubscriptions");

                    b.HasIndex("CustomerId")
                        .IsUnique()
                        .HasDatabaseName("iX_customerSubscriptions_customerId");

                    b.ToTable("customerSubscriptions");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("integer")
                        .HasColumnName("categoryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("name");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(8, 2)")
                        .HasColumnName("price");

                    b.HasKey("Id")
                        .HasName("pK_products");

                    b.HasIndex("CategoryId")
                        .HasDatabaseName("iX_products_categoryId");

                    b.ToTable("products");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.ProductCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("IconName")
                        .HasColumnType("varchar")
                        .HasColumnName("categoryIconPath");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("pK_productCategories");

                    b.ToTable("productCategories");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.ProductInventory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("InStoreQuantity")
                        .HasColumnType("integer")
                        .HasColumnName("inStoreQuantity");

                    b.Property<int>("OrderedQuantity")
                        .HasColumnType("integer")
                        .HasColumnName("orderedQuantity");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("productId");

                    b.Property<int>("SuretyQuantity")
                        .HasColumnType("integer")
                        .HasColumnName("suretyQuantity");

                    b.HasKey("Id")
                        .HasName("pK_productInventory");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("iX_productInventory_productId");

                    b.ToTable("productInventory");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.ProductPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("integer")
                        .HasColumnName("customerId");

                    b.Property<bool>("IsRefunded")
                        .HasColumnType("boolean")
                        .HasColumnName("isRefunded");

                    b.Property<int>("ProductId")
                        .HasColumnType("integer")
                        .HasColumnName("productId");

                    b.Property<decimal>("SellingPrice")
                        .HasColumnType("decimal(8, 2)")
                        .HasColumnName("sellingPrice");

                    b.HasKey("Id")
                        .HasName("pK_productPayments");

                    b.HasIndex("CustomerId")
                        .HasDatabaseName("iX_productPayments_customerId");

                    b.HasIndex("ProductId")
                        .HasDatabaseName("iX_productPayments_productId");

                    b.ToTable("productPayments");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.CustomerEntries", b =>
                {
                    b.HasOne("BoulderPOS.API.Models.Customer", "Customer")
                        .WithOne("Entries")
                        .HasForeignKey("BoulderPOS.API.Models.CustomerEntries", "CustomerId")
                        .HasConstraintName("fK_customerEntries_customers_customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.CustomerSubscription", b =>
                {
                    b.HasOne("BoulderPOS.API.Models.Customer", "Customer")
                        .WithOne("Subscription")
                        .HasForeignKey("BoulderPOS.API.Models.CustomerSubscription", "CustomerId")
                        .HasConstraintName("fK_customerSubscriptions_customers_customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.Product", b =>
                {
                    b.HasOne("BoulderPOS.API.Models.ProductCategory", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .HasConstraintName("fK_products_productCategories_categoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.ProductInventory", b =>
                {
                    b.HasOne("BoulderPOS.API.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fK_productInventory_products_productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.ProductPayment", b =>
                {
                    b.HasOne("BoulderPOS.API.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .HasConstraintName("fK_productPayments_customers_customerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BoulderPOS.API.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("fK_productPayments_products_productId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.Customer", b =>
                {
                    b.Navigation("Entries");

                    b.Navigation("Orders");

                    b.Navigation("Subscription");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.Product", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("BoulderPOS.API.Models.ProductCategory", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
