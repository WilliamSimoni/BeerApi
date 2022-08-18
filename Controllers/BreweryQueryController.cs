using Contracts.Dtos;
using Domain.Common.Errors.Base;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BreweryDto>))]
        public async Task<ActionResult<IEnumerable<BreweryDto>>> GetAllBreweries()
        {
            var breweries = await _services.QueryBrewery.GetAll();

            return Ok(breweries);
        }

        [HttpGet("{breweryId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BreweryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BreweryDto>> GetBreweryById(int breweryId)
        {
            var serviceResult = await _services.QueryBrewery.GetById(breweryId);

            return serviceResult.Match<ActionResult>(
                brewery => Ok(brewery),
                notFoundError => NotFound(notFoundError.Message)
                );
        }

        [HttpGet("{breweryId}/beers")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BeerDto>))]
        public async Task<ActionResult<IEnumerable<BeerDto>>> GetAllBeersFromBrewery(int breweryId)
        {
            var serviceResult = await _services.QueryBreweryBeers.GetAllBeers(breweryId);

            return serviceResult.Match<ActionResult>(
                beers => Ok(beers),
                notFoundError => NotFound(notFoundError.Message)
                );
        }

        [HttpGet("{breweryId}/beers/{beerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BeerDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BeerDto>> GetBeerByIdFromBrewery(int breweryId, int beerId)
        {
            var serviceResult = await _services.QueryBreweryBeers.GetBeerById(breweryId, beerId);

            return serviceResult.Match<ActionResult>(
                    beer => Ok(beer),
                    notFoundError => NotFound(notFoundError.Message)
                );
        }

    }
}