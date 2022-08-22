using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/wholesalers")]
    [ApiController]
    public class WholesalerQueryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public WholesalerQueryController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet("{wholesalerId}/beers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetInventoryBeerDto>>> GetAllBeers(int wholesalerId)
        {
            var serviceResult = await _services.QueryWholesaler.GetAllWholesalerBeers(wholesalerId);

            return serviceResult.Match<ActionResult>(
                beers => Ok(beers),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }

        [HttpGet("{wholesalerId}/beers/{beerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetInventoryBeerDto>> GetBeerById(int wholesalerId, int beerId)
        {
            var serviceResult = await _services.QueryWholesaler.GetWholesalerBeerById(wholesalerId, beerId);

            return serviceResult.Match<ActionResult>(
                beer => Ok(beer),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }
    }
}
