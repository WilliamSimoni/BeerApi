using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;
using OneOf;

namespace BeerApi.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SaleCommandController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public SaleCommandController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> insertSale([FromBody] ForCreationSaleDto saleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serviceResult = await _services.ChangeSale.addSale(saleDto);

            return serviceResult.Match(
                newSale => Created(nameof(SaleQueryController.GetSaleById), newSale),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );

        }
    }
}
