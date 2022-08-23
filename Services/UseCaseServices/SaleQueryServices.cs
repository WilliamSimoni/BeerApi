using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Entities;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using OneOf;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    public class SaleQueryServices : ISaleQueryServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public SaleQueryServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetSaleDto>> GetAll()
        {
            var sales = await _unitOfWork.QuerySale.GetAll();

            _logger.LogDebug("QuerySaleService retrieved all the sales");

            return _mapper.Map<GetSaleDto[]>(sales);
        }

        public async Task<OneOf<GetBeerFromSaleDto, IError>> GetBeerInvolvedInSale(int saleId)
        {
            var sale = await _unitOfWork.QuerySale.GetByCondition(
                s => s.SaleId == saleId 
                );

            if (!sale.Any())
            {
                _logger.LogInfo("QuerySaleService tried to retrieve the beer involved in a sale, but id {1} is not associated with any sale.", saleId);
                return new SaleNotFound(saleId);
            }

            _logger.LogDebug("QuerySaleService retrieved sale with id {1}. So, it can get the information about the beer involved in the sale", saleId);

            var beer = await _unitOfWork.QueryBeer.GetByCondition(b => b.BeerId == sale.First().BeerId);

            _logger.LogDebug("QuerySaleService retrieved beer associated to sale with id {1}", saleId);

            return _mapper.Map<GetBeerFromSaleDto>(beer.First());
        }

        public async Task<OneOf<GetSaleDto, IError>> GetById(int saleId)
        {
            var sale = await _unitOfWork.QuerySale.GetByCondition(
                s => s.SaleId == saleId);

            if (!sale.Any())
            {
                _logger.LogInfo("QuerySaleService tried to retrieve a sale, but id {1} is not associated with any sale", saleId);
                return new SaleNotFound(saleId);
            }

            _logger.LogDebug("QuerySaleService retrieved sale with id {1}", saleId);

            return _mapper.Map<GetSaleDto>(sale.First());
        }
    }
}