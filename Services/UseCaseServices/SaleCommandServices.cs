using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Entities;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using OneOf;
using Services.Abstract.UseCaseServices;
using static Domain.Common.Errors.ErrorsWholesaler;

namespace Services.UseCaseServices
{
    public class SaleCommandServices : ISaleCommandServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public SaleCommandServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<CreatedSaleDto, IError>> addSale(ForCreationSaleDto creationSaleDto)
        {
            var newSale = _mapper.Map<Sale>(creationSaleDto);

            _logger.LogDebug("SaleCommandService mapped ForCreationSaleDto to BeerDto");

            //check if the wholesaler exists
            var wholesaler = await _unitOfWork.QueryWholesaler
                .GetByCondition(w => w.WholesalerId == newSale.WholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("SaleCommandService tried to add a sale, but the wholesaler with {1} does not exist", newSale.WholesalerId);
                return new BadWholesalerId(newSale.WholesalerId);
            }

            _logger.LogDebug("SaleCommandService found wholesaler with id {1}", newSale.WholesalerId);

            //check if the beer exists
            var beer = await _unitOfWork.QueryBeer
                .GetByCondition(b => b.BeerId == newSale.BeerId && b.InProduction == true);

            if (!beer.Any())
            {
                _logger.LogInfo("SaleCommandService tried to add a sale, but the beer with {1} does not exist", newSale.BeerId);
                return new BadBeerId(newSale.BeerId);
            }

            _logger.LogDebug("SaleCommandService found beer with id {1}", newSale.BeerId);

            //add the sale
            _unitOfWork.ChangeSale.Add(newSale);
            int result = await _unitOfWork.SaveAsync();

            if (result == 1)
            {
                _logger.LogError("SaleCommandService was not able to add a new sale to the repository", newSale.SaleId);
                return new SaleInsertionInternalError();
            }

            _logger.LogDebug("SaleCommandService added a new sale with id {1} to the repository");

            return _mapper.Map<CreatedSaleDto>(newSale);
        }
    }
}