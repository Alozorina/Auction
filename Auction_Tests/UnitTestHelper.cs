using AutoMapper;
using BLL;
using DAL.Data;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;

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
            context.Users.AddRange(
                new User { Id = 1, FirstName = "Name1", LastName = "Surname1", Email = "user1@mm.com", 
                           BirthDate = new DateTime(1980, 5, 25), Password = "password1", RoleId = 1});
            context.SaveChanges();
        }
    }
}