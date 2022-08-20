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
            //check if the wholesaler exists
            var wholesaler = await _unitOfWork.QueryWholesaler
                .GetByCondition(w => w.WholesalerId == creationSaleDto.WholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("CommandSaleService tried to add a sale, but the wholesaler with {1} does not exist", creationSaleDto.WholesalerId);
                return new BadWholesalerId(creationSaleDto.WholesalerId);
            }

            _logger.LogDebug("CommandSaleService found wholesaler with id {1}", creationSaleDto.WholesalerId);

            //check if the beer exists
            var beer = await _unitOfWork.QueryBeer
                .GetByCondition(b => b.BeerId == creationSaleDto.BeerId && b.InProduction == true);

            if (!beer.Any())
            {
                _logger.LogInfo("CommandSaleService tried to add a sale, but the beer with {1} does not exist", creationSaleDto.BeerId);
                return new BadBeerId(creationSaleDto.BeerId);
            }

            _logger.LogDebug("CommandSaleService found beer with id {1}", creationSaleDto.BeerId);

            //add the sale
            var newSale = _mapper.Map<Sale>(creationSaleDto);
            _unitOfWork.ChangeSale.Add(newSale);
            await _unitOfWork.saveAsync();

            _logger.LogDebug("CommandSaleService added a new sale with id {1} to the repository", newSale.SaleId);

            return _mapper.Map<CreatedSaleDto>(newSale);
        }
    }
}