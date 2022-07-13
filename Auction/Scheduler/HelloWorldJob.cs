using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System.Threading.Tasks;

namespace Auction.Scheduler
{
    [DisallowConcurrentExecution]
    public class HelloWorldJob : IJob
    {
        private readonly ILogger<HelloWorldJob> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HelloWorldJob(ILogger<HelloWorldJob> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public Task Execute(IJobExecutionContext context)
        {
            _logger.LogInformation("Hello world!");
            return Task.CompletedTask;
        }
    }
}
