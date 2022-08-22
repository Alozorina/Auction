using Auction.Scheduler;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace Auction
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            })
            .ConfigureServices((hostContext, services) =>
            {
                // Add the required Quartz.NET services
                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionScopedJobFactory();

                    // Create a "key" for the job
                    var jobKeyClose = new JobKey("UpdateItemCloseStatusJob");
                    var jobKeyOpen = new JobKey("UpdateItemOpenStatusJob");

                    // Register the job with the DI container
                    q.AddJob<UpdateItemCloseStatusJob>(opts => opts.WithIdentity(jobKeyClose));
                    q.AddJob<UpdateItemOpenStatusJob>(opts => opts.WithIdentity(jobKeyOpen));

                    // Create a trigger for the job
                    q.AddTrigger(opts => opts
                        .ForJob(jobKeyClose) // link to the UpdateItemCloseStatusJob
                        .WithIdentity("UpdateItemCloseStatusJob-trigger") // give the trigger a unique name
                        .WithCronSchedule("* 0/5 * * * ?")); // run every 5 min

                    q.AddTrigger(opts => opts
                        .ForJob(jobKeyOpen)
                        .WithIdentity("UpdateItemOpenStatusJob-trigger")
                        .WithCronSchedule("* 0/5 * * * ?"));

                });
                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = true);

                // Add the Quartz.NET hosted service
                services.AddQuartzHostedService(
                    q => q.WaitForJobsToComplete = true);
            });
    }
}
