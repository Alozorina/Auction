using DAL.Entities;
using DAL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Auction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuctionController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly IUnitOfWork _unitOfWork;

        public AuctionController(ILogger<AuctionController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuctionEntity>>> GetAll()
        {
            var auctions = await _unitOfWork.AuctionRepository.GetAllWithDetailsAsync();
            return Ok(auctions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionEntity>> GetById(int id)
        {
            var user = await _unitOfWork.UserRepository.GetByIdAsync(id);

            if (user == null)
            {
                _logger.Log(LogLevel.Error, $"Account with id: {id}, hasn't been found in db.");
                return Unauthorized();
            }

            return Ok(user);
        }
    }
}
