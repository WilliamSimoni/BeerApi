using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/sales")]
    [ApiController]
    public class SaleQueryController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public SaleQueryController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GetSaleDto>))]
        public async Task<ActionResult<IEnumerable<GetSaleDto>>> getAllSales()
        {
            var sales = await _services.QuerySale.GetAll();

            return Ok(sales);
        }

        [HttpGet("{saleId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetSaleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetSaleDto>> getSaleById(int saleId)
        {
            var serviceResult = await _services.QuerySale.GetById(saleId);

            return serviceResult.Match(
                sale => Ok(sale),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }

        [HttpGet("{saleId}/beer")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GetBeerFromSaleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetBeerFromSaleDto>> getBeerInvolvedInSale(int saleId)
        {
            var serviceResult = await _services.QuerySale.GetBeerInvolvedInSale(saleId);

            return serviceResult.Match(
                beer => Ok(beer),
                error => Problem(statusCode: error.Number, detail: error.Message)
                );
        }

    }
}
