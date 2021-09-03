﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TaxCalculatorDB;

namespace TaxCalculatorDB.Migrations
{
    [DbContext(typeof(TaxCalculationDbContext))]
    [Migration("20210901104339_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TaxCalculatorDB.Models.PostalCodeTaxCalculation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TaxCalculationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("PostalCodeTaxCalculationLinks");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            PostalCode = "7441",
                            TaxCalculationType = 1
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            PostalCode = "A100",
                            TaxCalculationType = 2
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            PostalCode = "7000",
                            TaxCalculationType = 3
                        },
                        new
                        {
                            Id = 4L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            IsActive = true,
                            PostalCode = "1000",
                            TaxCalculationType = 1
                        });
                });

            modelBuilder.Entity("TaxCalculatorDB.Models.TaxBand", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<int>("FromBand")
                        .HasColumnType("int");

                    b.Property<decimal>("Rate")
                        .HasPrecision(18, 4)
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("ToBand")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TaxBands");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            FromBand = 0,
                            Rate = 0.01m,
                            ToBand = 8350
                        },
                        new
                        {
                            Id = 2L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            FromBand = 8351,
                            Rate = 0.015m,
                            ToBand = 33950
                        },
                        new
                        {
                            Id = 3L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            FromBand = 33951,
                            Rate = 0.025m,
                            ToBand = 82250
                        },
                        new
                        {
                            Id = 4L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            FromBand = 82251,
                            Rate = 0.028m,
                            ToBand = 171550
                        },
                        new
                        {
                            Id = 5L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            FromBand = 171551,
                            Rate = 0.033m,
                            ToBand = 372950
                        },
                        new
                        {
                            Id = 6L,
                            CreatedAt = new DateTime(2021, 8, 28, 5, 0, 0, 0, DateTimeKind.Utc),
                            FromBand = 372951,
                            Rate = 0.035m,
                            ToBand = 2147483647
                        });
                });

            modelBuilder.Entity("TaxCalculatorDB.Models.TaxCalculation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<decimal>("AnnualIncome")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Result")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("TaxCalculationType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("TaxCalculations");
                });
#pragma warning restore 612, 618
        }
    }
}
