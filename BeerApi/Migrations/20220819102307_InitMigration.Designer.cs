﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Repositories.DataContext;

#nullable disable

namespace BeerApi.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220819102307_InitMigration")]
    partial class InitMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Domain.Entities.Beer", b =>
                {
                    b.Property<int>("BeerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeerId"), 1L, 1);

                    b.Property<double>("AlcoholContent")
                        .HasColumnType("float");

                    b.Property<int>("BreweryId")
                        .HasColumnType("int");

                    b.Property<bool>("InProduction")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("OutOfProductionDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("SellingPriceToClients")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<decimal>("SellingPriceToWholesalers")
                        .HasColumnType("Decimal(10,2)");

                    b.HasKey("BeerId");

                    b.HasIndex("BreweryId");

                    b.HasIndex("Name", "OutOfProductionDate", "BreweryId")
                        .IsUnique();

                    b.ToTable("Beer", (string)null);

                    b.HasData(
                        new
                        {
                            BeerId = 1,
                            AlcoholContent = 11.0,
                            BreweryId = 1,
                            InProduction = true,
                            Name = "forte hendrik quadrupel",
                            OutOfProductionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPriceToClients = 10.99m,
                            SellingPriceToWholesalers = 3.99m
                        },
                        new
                        {
                            BeerId = 2,
                            AlcoholContent = 6.0,
                            BreweryId = 1,
                            InProduction = true,
                            Name = "brugse zot blond",
                            OutOfProductionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPriceToClients = 12.99m,
                            SellingPriceToWholesalers = 4.99m
                        },
                        new
                        {
                            BeerId = 3,
                            AlcoholContent = 0.40000000000000002,
                            BreweryId = 1,
                            InProduction = true,
                            Name = "sportzot",
                            OutOfProductionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPriceToClients = 6.99m,
                            SellingPriceToWholesalers = 1.59m
                        },
                        new
                        {
                            BeerId = 4,
                            AlcoholContent = 5.0,
                            BreweryId = 2,
                            InProduction = true,
                            Name = "bourgogne des flandres",
                            OutOfProductionDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            SellingPriceToClients = 2.59m,
                            SellingPriceToWholesalers = 0.29m
                        },
                        new
                        {
                            BeerId = 5,
                            AlcoholContent = 7.5,
                            BreweryId = 1,
                            InProduction = false,
                            Name = "Brugse Zot Dubbel",
                            OutOfProductionDate = new DateTime(2022, 5, 9, 9, 15, 0, 0, DateTimeKind.Unspecified),
                            SellingPriceToClients = 19.99m,
                            SellingPriceToWholesalers = 8.99m
                        });
                });

            modelBuilder.Entity("Domain.Entities.BeerSale", b =>
                {
                    b.Property<int>("BeerSaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BeerSaleId"), 1L, 1);

                    b.Property<int>("BeerId")
                        .HasColumnType("int");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<int>("NumberOfUnits")
                        .HasColumnType("int");

                    b.Property<decimal>("PricePerUnit")
                        .HasColumnType("Decimal(10,2)");

                    b.Property<int>("SaleId")
                        .HasColumnType("int");

                    b.HasKey("BeerSaleId");

                    b.HasIndex("BeerId");

                    b.HasIndex("SaleId");

                    b.ToTable("BeerSale", (string)null);

                    b.HasData(
                        new
                        {
                            BeerSaleId = 1,
                            BeerId = 1,
                            Discount = 0,
                            NumberOfUnits = 1000,
                            PricePerUnit = 3.99m,
                            SaleId = 1
                        },
                        new
                        {
                            BeerSaleId = 2,
                            BeerId = 5,
                            Discount = 0,
                            NumberOfUnits = 1000,
                            PricePerUnit = 8.99m,
                            SaleId = 1
                        },
                        new
                        {
                            BeerSaleId = 3,
                            BeerId = 2,
                            Discount = 0,
                            NumberOfUnits = 200,
                            PricePerUnit = 4.99m,
                            SaleId = 2
                        },
                        new
                        {
                            BeerSaleId = 4,
                            BeerId = 1,
                            Discount = 0,
                            NumberOfUnits = 300,
                            PricePerUnit = 3.99m,
                            SaleId = 3
                        },
                        new
                        {
                            BeerSaleId = 5,
                            BeerId = 2,
                            Discount = 20,
                            NumberOfUnits = 2000,
                            PricePerUnit = 3.99m,
                            SaleId = 3
                        },
                        new
                        {
                            BeerSaleId = 6,
                            BeerId = 4,
                            Discount = 0,
                            NumberOfUnits = 200,
                            PricePerUnit = 0.29m,
                            SaleId = 3
                        },
                        new
                        {
                            BeerSaleId = 7,
                            BeerId = 2,
                            Discount = 0,
                            NumberOfUnits = 200,
                            PricePerUnit = 4.99m,
                            SaleId = 4
                        },
                        new
                        {
                            BeerSaleId = 8,
                            BeerId = 1,
                            Discount = 0,
                            NumberOfUnits = 100,
                            PricePerUnit = 3.99m,
                            SaleId = 5
                        },
                        new
                        {
                            BeerSaleId = 9,
                            BeerId = 2,
                            Discount = 0,
                            NumberOfUnits = 150,
                            PricePerUnit = 4.99m,
                            SaleId = 5
                        },
                        new
                        {
                            BeerSaleId = 10,
                            BeerId = 3,
                            Discount = 12,
                            NumberOfUnits = 1000,
                            PricePerUnit = 1.39m,
                            SaleId = 5
                        });
                });

            modelBuilder.Entity("Domain.Entities.Brewery", b =>
                {
                    b.Property<int>("BreweryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BreweryId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("BreweryId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Brewery", (string)null);

                    b.HasData(
                        new
                        {
                            BreweryId = 1,
                            Address = "walplein 26 8000 brugge",
                            Email = "info@halvemaan.be",
                            Name = "huisbrouwerij de halve maan"
                        },
                        new
                        {
                            BreweryId = 2,
                            Address = "kartuizerinnenstraat 6 8000 brugge",
                            Email = "visits@bourgognedesflandres",
                            Name = "bourgogne des flandres"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Sale", b =>
                {
                    b.Property<int>("SaleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SaleId"), 1L, 1);

                    b.Property<DateTime>("SaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("Decimal(18,2)");

                    b.Property<int>("WholesalerId")
                        .HasColumnType("int");

                    b.HasKey("SaleId");

                    b.HasIndex("WholesalerId");

                    b.ToTable("Sale", (string)null);

                    b.HasData(
                        new
                        {
                            SaleId = 1,
                            SaleDate = new DateTime(2021, 10, 4, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 12980m,
                            WholesalerId = 1
                        },
                        new
                        {
                            SaleId = 2,
                            SaleDate = new DateTime(2022, 1, 2, 12, 30, 0, 0, DateTimeKind.Unspecified),
                            Total = 998m,
                            WholesalerId = 2
                        },
                        new
                        {
                            SaleId = 3,
                            SaleDate = new DateTime(2022, 8, 6, 18, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 9235m,
                            WholesalerId = 1
                        },
                        new
                        {
                            SaleId = 4,
                            SaleDate = new DateTime(2022, 2, 3, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 998m,
                            WholesalerId = 2
                        },
                        new
                        {
                            SaleId = 5,
                            SaleDate = new DateTime(2022, 2, 3, 16, 0, 0, 0, DateTimeKind.Unspecified),
                            Total = 2537.5m,
                            WholesalerId = 3
                        });
                });

            modelBuilder.Entity("Domain.Entities.Wholesaler", b =>
                {
                    b.Property<int>("WholesalerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("WholesalerId"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("WholesalerId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Wholesaler", (string)null);

                    b.HasData(
                        new
                        {
                            WholesalerId = 1,
                            Address = "jump street 21",
                            Email = "info@thebeer.be",
                            Name = "thebeer"
                        },
                        new
                        {
                            WholesalerId = 2,
                            Address = "evergreen street 32",
                            Email = "contact@berallaxcorp.com",
                            Name = "berallax corp"
                        },
                        new
                        {
                            WholesalerId = 3,
                            Address = "sesame street 77",
                            Email = "thebeercorporationinfo@beercorp.com",
                            Name = "the beer corporation"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Beer", b =>
                {
                    b.HasOne("Domain.Entities.Brewery", "Brewery")
                        .WithMany("Beers")
                        .HasForeignKey("BreweryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Brewery");
                });

            modelBuilder.Entity("Domain.Entities.BeerSale", b =>
                {
                    b.HasOne("Domain.Entities.Beer", "Beer")
                        .WithMany("BeerSales")
                        .HasForeignKey("BeerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Sale", "Sale")
                        .WithMany()
                        .HasForeignKey("SaleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Beer");

                    b.Navigation("Sale");
                });

            modelBuilder.Entity("Domain.Entities.Sale", b =>
                {
                    b.HasOne("Domain.Entities.Wholesaler", "Wholesaler")
                        .WithMany("Sales")
                        .HasForeignKey("WholesalerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Wholesaler");
                });

            modelBuilder.Entity("Domain.Entities.Beer", b =>
                {
                    b.Navigation("BeerSales");
                });

            modelBuilder.Entity("Domain.Entities.Brewery", b =>
                {
                    b.Navigation("Beers");
                });

            modelBuilder.Entity("Domain.Entities.Wholesaler", b =>
                {
                    b.Navigation("Sales");
                });
#pragma warning restore 612, 618
        }
    }
}
