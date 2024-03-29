﻿// <auto-generated />
using System;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Data.Migrations
{
    [DbContext(typeof(AuctionDbContext))]
    partial class AuctionDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.26")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Data.Entities.Category", b =>
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

            modelBuilder.Entity("Data.Entities.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BuyerId")
                        .HasColumnType("int");

                    b.Property<string>("CreatedBy")
                        .IsRequired()
                        .HasColumnType("nvarchar(80)")
                        .HasMaxLength(80);

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
                            CreatedBy = "Steve Johnson",
                            CurrentBid = 0m,
                            Description = "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.",
                            EndSaleDate = new DateTime(2023, 8, 15, 22, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Blue Marble",
                            OwnerId = 1,
                            StartSaleDate = new DateTime(2023, 5, 22, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 50m,
                            StatusId = 2
                        },
                        new
                        {
                            Id = 2,
                            CreatedBy = "Steve Johnson",
                            CurrentBid = 0m,
                            Description = "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.",
                            EndSaleDate = new DateTime(2023, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Revolution",
                            OwnerId = 2,
                            StartSaleDate = new DateTime(2022, 12, 12, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 60m,
                            StatusId = 2
                        },
                        new
                        {
                            Id = 3,
                            CreatedBy = "Steve Johnson",
                            CurrentBid = 40m,
                            Description = "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.",
                            EndSaleDate = new DateTime(2023, 8, 15, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Sunset",
                            OwnerId = 2,
                            StartSaleDate = new DateTime(2023, 3, 9, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 30m,
                            StatusId = 4
                        },
                        new
                        {
                            Id = 4,
                            BuyerId = 11,
                            CreatedBy = "Steve Johnson",
                            CurrentBid = 20m,
                            Description = "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.",
                            EndSaleDate = new DateTime(2022, 8, 3, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Spinning Around",
                            OwnerId = 6,
                            StartSaleDate = new DateTime(2022, 6, 14, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 5m,
                            StatusId = 5
                        },
                        new
                        {
                            Id = 5,
                            CreatedBy = "Steve Johnson",
                            CurrentBid = 70m,
                            Description = "Following the success of the inaugural edition in March 2021, consignments are now open for the second iteration of Veiling Steve Johnson's Bright Colors auction this July. Running the gamut of the avant-garde from the late 19th century through to artists working today, the sale presents exceptional artworks by artists who dared to innovate and experiment over the course of 150 years.",
                            EndSaleDate = new DateTime(2023, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Antarctica Is Changing",
                            OwnerId = 6,
                            StartSaleDate = new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 60m,
                            StatusId = 4
                        },
                        new
                        {
                            Id = 6,
                            CreatedBy = "Jesse Zheng",
                            CurrentBid = 70m,
                            EndSaleDate = new DateTime(2023, 8, 10, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Green Hills",
                            OwnerId = 6,
                            StartSaleDate = new DateTime(2022, 7, 9, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 60m,
                            StatusId = 4
                        },
                        new
                        {
                            Id = 7,
                            CreatedBy = "Pawel Czerwinski",
                            CurrentBid = 0m,
                            EndSaleDate = new DateTime(2023, 8, 19, 12, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Black Gold",
                            OwnerId = 11,
                            StartSaleDate = new DateTime(2023, 8, 9, 11, 0, 0, 0, DateTimeKind.Unspecified),
                            StartingPrice = 60m,
                            StatusId = 2
                        });
                });

            modelBuilder.Entity("Data.Entities.ItemCategory", b =>
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
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 2,
                            ItemId = 3
                        },
                        new
                        {
                            Id = 6,
                            CategoryId = 2,
                            ItemId = 4
                        },
                        new
                        {
                            Id = 7,
                            CategoryId = 2,
                            ItemId = 5
                        },
                        new
                        {
                            Id = 8,
                            CategoryId = 1,
                            ItemId = 2
                        },
                        new
                        {
                            Id = 9,
                            CategoryId = 1,
                            ItemId = 1
                        },
                        new
                        {
                            Id = 10,
                            CategoryId = 1,
                            ItemId = 3
                        },
                        new
                        {
                            Id = 11,
                            CategoryId = 1,
                            ItemId = 4
                        },
                        new
                        {
                            Id = 12,
                            CategoryId = 1,
                            ItemId = 5
                        });
                });

            modelBuilder.Entity("Data.Entities.ItemPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

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
                            Path = "steve-johnson-unsplash.jpg"
                        },
                        new
                        {
                            Id = 2,
                            ItemId = 2,
                            Path = "pexels-steve-johnson-1840624.jpg"
                        },
                        new
                        {
                            Id = 3,
                            ItemId = 3,
                            Path = "pexels-steve-johnson-1174000.jpg"
                        },
                        new
                        {
                            Id = 4,
                            ItemId = 4,
                            Path = "pexels-steve-johnson-1286632.jpg"
                        },
                        new
                        {
                            Id = 5,
                            ItemId = 5,
                            Path = "steve-johnson-RzykwoNjoLw-unsplash.jpg"
                        },
                        new
                        {
                            Id = 6,
                            ItemId = 5,
                            Path = "steve-johnson-RzykwoNjoLw-unsplash-mockup.jpg"
                        },
                        new
                        {
                            Id = 7,
                            ItemId = 6,
                            Path = "pexels-jesse-zheng-732548.jpg"
                        },
                        new
                        {
                            Id = 8,
                            ItemId = 7,
                            Path = "pawel-czerwinski-xubOAAKUwXc-unsplash.jpg"
                        });
                });

            modelBuilder.Entity("Data.Entities.Role", b =>
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

            modelBuilder.Entity("Data.Entities.Status", b =>
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
                            Name = "OnApproval"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Upcoming"
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

            modelBuilder.Entity("Data.Entities.User", b =>
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
                        .HasColumnType("nvarchar(max)");

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
                            Password = "$2a$11$JWv4B7tq4YQiTaAI1dz47.kMnqCcJPL9d1R1TlWlsXyPOn6271PNe",
                            RoleId = 2
                        },
                        new
                        {
                            Id = 2,
                            BirthDate = new DateTime(2000, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "johnmail@mail.com",
                            FirstName = "John",
                            LastName = "Doe",
                            Password = "$2a$11$ki6XQ7oAs4XqO.MoWdv6He9iCtYfsa.YutDzV/lgHxQSUmHkrsLUa",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 6,
                            BirthDate = new DateTime(1980, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "peter@mail.com",
                            FirstName = "Peter",
                            LastName = "Choi",
                            Password = "$2a$11$xQ0LSLPRNqoGkE4cD0.o..XKrhgIvICD1PxyYhypFfMhmOnLjG7s6",
                            RoleId = 1
                        },
                        new
                        {
                            Id = 11,
                            BirthDate = new DateTime(1997, 8, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "dana@mail.com",
                            FirstName = "Dana",
                            LastName = "Meng",
                            Password = "$2a$11$9LHSXUXlE5sGwIhfzTrnx.dJ0zcJEJBXMhbofOAJ0XewcWCSy6byy",
                            RoleId = 1
                        });
                });

            modelBuilder.Entity("Data.Entities.Item", b =>
                {
                    b.HasOne("Data.Entities.User", "Buyer")
                        .WithMany("Purchases")
                        .HasForeignKey("BuyerId");

                    b.HasOne("Data.Entities.User", "Owner")
                        .WithMany("Lots")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Status", "Status")
                        .WithMany("Items")
                        .HasForeignKey("StatusId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.ItemCategory", b =>
                {
                    b.HasOne("Data.Entities.Category", "Category")
                        .WithMany("ItemCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Data.Entities.Item", "Item")
                        .WithMany("ItemCategories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.ItemPhoto", b =>
                {
                    b.HasOne("Data.Entities.Item", null)
                        .WithMany("ItemPhotos")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Data.Entities.User", b =>
                {
                    b.HasOne("Data.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
