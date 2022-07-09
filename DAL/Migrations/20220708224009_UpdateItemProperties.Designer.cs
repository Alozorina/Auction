﻿// <auto-generated />
using System;
using DAL.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    [Migration("20220708224009_UpdateItemProperties")]
    partial class UpdateItemProperties
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("DAL.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Painting"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Steve Johnson"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Sculpture"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Ceramics"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Chinese Art"
                        },
                        new
                        {
                            Id = 6,
                            Name = "Porcelain"
                        },
                        new
                        {
                            Id = 7,
                            Name = "Jewelry"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<decimal>("CurrentBid")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(1000)")
                        .HasMaxLength(1000);

                    b.Property<DateTime>("EndSaleDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartSaleDate")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("StartingPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("StatusId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BuyerId");

                    b.HasIndex("OwnerId");

                    b.HasIndex("StatusId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CurrentBid = 0m,
                            EndSaleDate = new DateTime(2022, 8, 15, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Blue Marble",
                            OwnerId = 1,
                            StartSaleDate = new DateTime(2022, 7, 22, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 50m,
                            StatusId = 1
                        },
                        new
                        {
                            Id = 2,
                            CurrentBid = 0m,
                            EndSaleDate = new DateTime(2022, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Revolution",
                            OwnerId = 2,
                            StartSaleDate = new DateTime(2022, 8, 12, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 60m,
                            StatusId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entities.ItemCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemCategories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 1,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 2,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 2,
                            ItemId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entities.ItemPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.ToTable("ItemPhoto");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ItemId = 1,
                            Name = "steve-johnson-unsplash.jpg",
                            Path = "/images/"
                        },
                        new
                        {
                            Id = 2,
                            ItemId = 2,
                            Name = "pexels-steve-johnson-1840624.jpg",
                            Path = "/images/"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "User"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Admin"
                        });
                });

            modelBuilder.Entity("DAL.Entities.Status", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "On Approval"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Approved"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Rejected"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Open"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Closed"
                        });
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(32)")
                        .HasMaxLength(32);

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("User");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BirthDate = new DateTime(2000, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "janemail@mail.com",
                            FirstName = "Jane",
                            LastName = "Doe",
                            Password = "passwordJane",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johnmail@mail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "passwordJohn",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("DAL.Entities.Item", b =>
                {
                    b.HasOne("DAL.Entities.User", "Buyer")
                        .WithMany("Purchases")
                        .HasForeignKey("BuyerId");

                    b.HasOne("DAL.Entities.User", "Owner")
                        .WithMany("Lots")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Status", "Status")
                        .WithMany("Items")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.ItemCategory", b =>
                {
                    b.HasOne("DAL.Entities.Category", "Category")
                        .WithMany("ItemCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DAL.Entities.Item", "Item")
                        .WithMany("ItemCategories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.ItemPhoto", b =>
                {
                    b.HasOne("DAL.Entities.Item", "Item")
                        .WithMany("ItemPhotos")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DAL.Entities.User", b =>
                {
                    b.HasOne("DAL.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
