﻿using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/breweries")]
    [ApiController]
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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> AddBeerToBrewery(int breweryId, [FromBody] ForCreationBeerDto creationBeerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _services.ChangeBreweryBeers.AddBeerToBrewery(breweryId, creationBeerDto);

            return result.Match(
                beerDto => Created($"/api/breweries/{breweryId}/beers/{beerDto.BeerId}", beerDto),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }

        [HttpDelete("{breweryId}/beers/{beerId}")]
        public async Task<ActionResult> removeBeerFromBrewery(int breweryId, int beerId)
        {
            var error = await _services.ChangeBreweryBeers.RemoveBeerFromBrewery(breweryId, beerId);

            if (error is null)
            {
                return NoContent();
            }

            return Problem(statusCode: error.Number, detail: error.Message);
        }

    }
}