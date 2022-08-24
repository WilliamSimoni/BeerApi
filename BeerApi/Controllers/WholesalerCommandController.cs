using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/wholesalers")]
    [ApiController]
    [ApiVersion("1.0")]
    public class WholesalerCommandController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public WholesalerCommandController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpPatch("{wholesalerId}/beers/{beerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateBeerQuantity(int wholesalerId, int beerId, [FromBody] ForUpdateInventoryBeerDto updateBeerDto)
        {

            var serviceResult = await _services.ChangeWholesaler.UpdateQuantity(wholesalerId, beerId, updateBeerDto);

            _logger.LogDebug("WholesalerCommandController received result from ChangeWholesaler.UpdateQuantity");

            return serviceResult.Match<ActionResult>(
                updatedInventoryDto => NoContent(),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }

    }
}
