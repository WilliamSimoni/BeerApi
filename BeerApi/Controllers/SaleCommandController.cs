using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/sales")]
    [ApiController]
    [ApiVersion("1.0")]
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
        public async Task<ActionResult> PostSale([FromBody] ForCreationSaleDto saleDto)
        {
            var serviceResult = await _services.ChangeSale.addSale(saleDto);

            _logger.LogDebug("SaleCommandController received result from ChangeSale.addSale");

            return serviceResult.Match(
                newSale => Created(nameof(SaleQueryController.GetSaleById), newSale),
                error =>
                {

                    if (error is BeerNotFound || error is WholesalerNotFound)
                    {
                        ModelState.AddModelError(error.Code, error.Message);
                        return ValidationProblem(ModelState);
                    }

                    return Problem(statusCode: error.Number, detail: error.Message);
                }
                );

        }
    }
}
