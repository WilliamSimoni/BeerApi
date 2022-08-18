using Contracts.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Services.Abstract;

namespace Controllers
{
    [ApiController]
    [Route("api/breweries")]
    public class BreweryQueryController : ControllerBase
    {
        private readonly ILogger<BreweryQueryController> _logger;
        private readonly IServicesWrapper _services;

        public BreweryQueryController(ILogger<BreweryQueryController> logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreweryDto>>> GetAllBreweries()
        {
            var breweries = await _services.QueryBrewery.GetAll();

            return Ok(breweries);
        }

        [HttpGet("{breweryId}")]
        public async Task<ActionResult<BreweryDto>> GetBreweryById(int breweryId)
        {
            var serviceResult = await _services.QueryBrewery.GetById(breweryId);

            return serviceResult.Match<ActionResult>(
                brewery => Ok(brewery),
                notFoundError => NotFound(notFoundError.Message)
                );
        }


    }
}