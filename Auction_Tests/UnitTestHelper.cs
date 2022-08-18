using AutoMapper;
using BLL;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace Auction_Tests
{
    public static class UnitTestHelper
    {
        public static DbContextOptions<AuctionDbContext> GetUnitTestDbOptions()
        {
            var options = new DbContextOptionsBuilder<AuctionDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            using (var context = new AuctionDbContext(options))
            {
                SeedData(context);
            }
            return options;
        }

        public static IMapper CreateMapperProfile()
        {
            var myProfile = new AutomapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));

            return new Mapper(configuration);
        }

        public static void SeedData(AuctionDbContext context)
        {
            context.Users.AddRange(users[0], users[1]);
            context.Items.AddRange(items[0], items[1]);
            context.Roles.AddRange(roles[0], roles[1]);
            context.Statuses.AddRange(statuses[0], statuses[1]);
            context.Categories.AddRange(categories[0], categories[1]);
            context.ItemCategories.AddRange(itemCategories[0], itemCategories[1]);
            context.SaveChanges();
        }

        public static List<User> users = new List<User>()
        {
            new User
            {
                Id = 1,
                FirstName = "Name1",
                LastName = "Surname1",
                Email = "user1@mm.com",
                BirthDate = new DateTime(1981, 1, 21),
                Password = "password1",
                RoleId = 1
            },
            new User
            {
                Id = 2,
                FirstName = "Name2",
                LastName = "Surname12",
                Email = "user2@mm.com",
                BirthDate = new DateTime(1992, 2, 22),
                Password = "password2",
                RoleId = 2
            }
        };
        public static List<Item> items = new List<Item>()
        {
            new Item
            {
                Id = 1,
                Name = "TestItem1",
                CreatedBy = "Test Author",
                CurrentBid = 70,
                StartingPrice = 60m,
                StatusId = 2,
                OwnerId = 1,
                StartSaleDate = new DateTime(2022, 07, 09, 10, 00, 00),
                EndSaleDate = new DateTime(2022, 08, 10, 12, 00, 00),
            },
            new Item
            {
                Id = 2,
                Name = "TestItem2",
                CreatedBy = "Test Author2",
                CurrentBid = 70,
                StartingPrice = 60m,
                StatusId = 1,
                OwnerId = 1,
                StartSaleDate = new DateTime(2022, 07, 09, 10, 00, 00),
                EndSaleDate = new DateTime(2022, 08, 10, 12, 00, 00),
            }
        };
        public static List<Role> roles = new List<Role>()
        {
            new Role
            {
                Id = 1,
                Name = "user"
            },
            new Role
            {
                Id = 2,
                Name = "admin"
            }
        };
        public static List<Status> statuses = new List<Status>()
        {
             new Status
            {
                Id = 1,
                Name = "Status1",
            },
            new Status
            {
                Id = 2,
                Name = "Status2",
            }
        };
        public static List<Category> categories = new List<Category>()
        {
            new Category
            {
                Id = 1,
                Name = "Category1",
            },
            new Category
            {
                Id = 2,
                Name = "Category2",
            }
        };
        public static List<ItemCategory> itemCategories = new List<ItemCategory>()
        {
            new ItemCategory()
            {
                Id = 1,
                CategoryId = 1,
                ItemId = 1
            },
            new ItemCategory()
            {
                Id = 2,
                CategoryId = 2,
                ItemId = 2
            }
        };
    }
}