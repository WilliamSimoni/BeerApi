using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using OneOf;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    public class QuoteServices : IQuoteServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public QuoteServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<QuoteSummaryDto, IError>> GetQuote(QuoteRequestDto quoteRequest)
        {
            //check if wholesaler exists
            var wholesaler = await _unitOfWork.QueryWholesaler.GetByCondition(w => w.WholesalerId == quoteRequest.WholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("QuoteService was trying to create a quote summary, but did not find the specified wholesaler with id {1}", quoteRequest.WholesalerId);
                return new WholesalerNotFound(quoteRequest.WholesalerId);
            }

            _logger.LogDebug("QuoteService found wholesaler with id {1}", quoteRequest.WholesalerId);

            ICollection<QuoteSummaryItemDto> quoteSummaryItems = new List<QuoteSummaryItemDto>();
            int totalBeers = 0;
            decimal totalPrice = 0;

            //for each beer, check if: - it is sold by the wholesaler, - there is enough beer in the wholesaler's stock
            foreach (var requestedBeer in quoteRequest.Beers)
            {
                var inventoryBeerQuery = await _unitOfWork.QueryInventoryBeer.GetBeerInfoByCondition(b => b.BeerId == requestedBeer.BeerId && b.WholesalerId == quoteRequest.WholesalerId);

                if (!inventoryBeerQuery.Any())
                {
                    _logger.LogInfo("QuoteService was trying to create a quote summary, but beer with id {1} is not sold by wholesaler with id {2}", requestedBeer.BeerId, quoteRequest.WholesalerId);
                    return new BeerNotSoldByWholesaler(quoteRequest.WholesalerId, requestedBeer.BeerId);
                }

                var inventoryBeer = inventoryBeerQuery.First();

                if (requestedBeer.Quantity > inventoryBeer.Quantity)
                {
                    _logger.LogInfo("QuoteService was trying to update a beer quantity, but the number of requested beers with id {1} exceeds the number of available beers in the wholesaler's stock ({2} > {3})",
                        requestedBeer.BeerId, inventoryBeer.Quantity, requestedBeer.Quantity);
                    return new QuantityOverUnitsInStock(requestedBeer.BeerId, inventoryBeer.Quantity, requestedBeer.Quantity);
                }

                //custom mapping from the tuple (QuoteRequestItemDto, InventoryBeer) to QuoteSummaryItemDto
                quoteSummaryItems.Add(_mapper.Map<QuoteSummaryItemDto>((requestedBeer, inventoryBeer)));

                totalPrice += quoteSummaryItems.Last().SubTotal;
                totalBeers += requestedBeer.Quantity;
            }

            _logger.LogDebug("QuoteService validated all the beer requests in the RequestQuoteDto");

            //check for discount
            decimal discount = 0;
            if (totalBeers >= 10)
            {
                discount = 10;
            }
            if (totalBeers >= 20)
            {
                discount = 20;
            }

            //map
            return _mapper.Map<QuoteSummaryDto>((quoteRequest, quoteSummaryItems, discount, totalPrice));
        }
    }
}
