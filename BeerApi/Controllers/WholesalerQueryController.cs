using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/wholesalers")]
    [ApiController]
    [ApiVersion("1.0")]
    public class WholesalerQueryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public WholesalerQueryController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<GetWholesalerDto>>> GetAllWholesalers()
        {
            var serviceResult =  await _services.QueryWholesaler.GetAllWholesalers();

            return Ok(serviceResult);
        }

        [HttpGet("{wholesalerId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetWholesalerDto>> GetWholesalerById(int wholesalerId)
        {
            var serviceResult = await _services.QueryWholesaler.GetWholesalerById(wholesalerId);

            return serviceResult.Match<ActionResult>(
                wholesalers => Ok(wholesalers),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }


        [HttpGet("{wholesalerId}/beers")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<GetInventoryBeerDto>>> GetAllBeers(int wholesalerId)
        {
            var serviceResult = await _services.QueryWholesaler.GetAllWholesalerBeers(wholesalerId);

            _logger.LogDebug("WholesalerQueryController received result from QueryWholesaler.GetAllWholesalerBeers");

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

            _logger.LogDebug("WholesalerQueryController received result from QueryWholesaler.GetWholesalerBeerById");

            return serviceResult.Match<ActionResult>(
                beer => Ok(beer),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }
    }
}
