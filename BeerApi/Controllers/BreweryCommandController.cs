using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/breweries")]
    [ApiController]
    [ApiVersion("1.0")]
    public class BreweryCommandController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public BreweryCommandController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpPost("{breweryId}/beers")]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(BeerDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> AddBeerToBrewery(int breweryId, [FromBody] ForCreationBeerDto creationBeerDto)
        {
            var result = await _services.ChangeBreweryBeers.AddBeerToBrewery(breweryId, creationBeerDto);

            _logger.LogDebug("BreweryCommandController received result from ChangeBreweryBeers.AddBeerToBrewery");

            return result.Match(
                beerDto => Created($"/api/breweries/{breweryId}/beers/{beerDto.BeerId}", beerDto),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }

        [HttpDelete("{breweryId}/beers/{beerId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> removeBeerFromBrewery(int breweryId, int beerId)
        {
            var error = await _services.ChangeBreweryBeers.RemoveBeerFromBrewery(breweryId, beerId);

            _logger.LogDebug("BreweryCommandController received result from ChangeBreweryBeers.RemoveBeerFromBrewery");

            if (error is null)
            {
                return NoContent();
            }

            return Problem(statusCode: error.Number, detail: error.Message);
        }

    }
}
