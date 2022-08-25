using Contracts.Dtos;
using Domain.Logger;
using Microsoft.AspNetCore.Mvc;
using Services.Abstract;

namespace BeerApi.Controllers
{
    [Route("api/quotes")]
    [ApiController]
    [ApiVersion("1.0")]
    public class QuoteController : ControllerBase
    {

        private readonly ILoggerManager _logger;
        private readonly IServicesWrapper _services;

        public QuoteController(ILoggerManager logger, IServicesWrapper services)
        {
            _logger = logger;
            _services = services;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(QuoteSummaryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateQuote(QuoteRequestDto quoteRequestDto)
        {
            var serviceResult = await _services.AskQuote.GetQuote(quoteRequestDto);

            _logger.LogDebug("QuoteController received result from AskQuote.GetQuote");

            return serviceResult.Match(
                quoteSummary => Ok(quoteSummary),
                error =>
                {
                    ModelState.AddModelError(error.Code, error.Message);
                    return ValidationProblem(ModelState);
                }
            );
        }

    }
}
