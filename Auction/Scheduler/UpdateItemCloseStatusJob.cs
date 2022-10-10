using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Auction.Scheduler
{
    [DisallowConcurrentExecution]
    public class UpdateItemCloseStatusJob : IJob
    {
        private readonly ILogger<UpdateItemCloseStatusJob> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemCloseStatusJob(ILogger<UpdateItemCloseStatusJob> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var items = await _unitOfWork.ItemRepository.FindByPredicateAsync(i => i.StatusId == 4);
            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item.EndSaleDate < DateTime.Now)
                    {
                        item.StatusId = 5;
                        _logger.LogInformation($"Item {item.Name} with ID {item.Id} is Closed");
                    }
                }
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
