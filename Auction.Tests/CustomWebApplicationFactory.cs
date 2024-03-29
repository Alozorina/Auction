﻿using Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Web;

namespace Auction.Tests
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder
                .UseContentRoot(TestHelper.GetDirectory())
                .ConfigureServices(services =>
                {
                    RemoveLibraryDbContext(services);

                    var serviceProvider = GetInMemoryServiceProvider();

                    services.AddDbContextPool<AuctionDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("TestDb");
                        options.UseInternalServiceProvider(serviceProvider);
                    });

                    using (var scope = services.BuildServiceProvider().CreateScope())
                    {
                        var context = scope.ServiceProvider.GetRequiredService<AuctionDbContext>();
                        TestHelper.SeedData(context);
                    }
                });
        }

        private static ServiceProvider GetInMemoryServiceProvider()
        {
            return new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();
        }

        private static void RemoveLibraryDbContext(IServiceCollection services)
        {
            var descriptor = services.SingleOrDefault(
                d => d.ServiceType ==
                     typeof(DbContextOptions<AuctionDbContext>));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }
        }
    }
}
