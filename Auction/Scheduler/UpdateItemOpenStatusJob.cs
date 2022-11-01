using DAL.Entities.Configuration;
using DAL.Interfaces;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Auction.Scheduler
{
    public class UpdateItemOpenStatusJob : IJob
    {
        private readonly ILogger<UpdateItemOpenStatusJob> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateItemOpenStatusJob(ILogger<UpdateItemOpenStatusJob> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var items = await _unitOfWork.ItemRepository.FindByPredicateAsync(i => i.StatusId == (int)Statuses.Upcoming);

            if (items != null)
            {
                foreach (var item in items)
                {
                    if (item.StartSaleDate < DateTime.Now)
                    {
                        item.StatusId = (int)Statuses.Open;
                        _logger.LogInformation($"Item {item.Name} with ID {item.Id} is Open");
                    }
                }
                await _unitOfWork.SaveAsync();
            }
        }
    }
}
