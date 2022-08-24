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
    public class WholesalerQueryServices : IWholesalerQueryServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public WholesalerQueryServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<IEnumerable<GetInventoryBeerDto>, IError>> GetAllWholesalerBeers(int wholesalerId)
        {
            var wholesaler = await _unitOfWork.QueryWholesaler.GetByCondition(w => w.WholesalerId == wholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("WholesalerQueryService was trying to retrieve all wholesaler beers but did not find the specified wholesaler with id {1}", wholesalerId);
                return new WholesalerNotFound(wholesalerId);
            }

            _logger.LogDebug("WholesalerQueryService found wholesaler with id {1}. So, it can proceed with the query", wholesalerId);

            var beers = await _unitOfWork.QueryInventoryBeer.GetBeerInfoByCondition(b => b.WholesalerId == wholesalerId);

            _logger.LogDebug("WholesalerQueryService successfully retrieved beers sold by the wholesaler with id {1}", wholesalerId);

            return _mapper.Map<GetInventoryBeerDto[]>(beers);
        }

        public async Task<IEnumerable<GetWholesalerDto>> GetAllWholesalers()
        {
            var wholesalers =  await _unitOfWork.QueryWholesaler.GetAll();
            
            _logger.LogDebug("WholesalerQueryService retrieved all the wholesalers information");
            
            return _mapper.Map<GetWholesalerDto[]>(wholesalers);
        }

        public async Task<OneOf<GetInventoryBeerDto, IError>> GetWholesalerBeerById(int wholesalerId, int beerId)
        {
            var wholesaler = await _unitOfWork.QueryWholesaler.GetByCondition(w => w.WholesalerId == wholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("WholesalerQueryService was trying to retrieve a wholesaler beer but did not find the specified wholesaler with id {1}", wholesalerId);
                return new WholesalerNotFound(wholesalerId);
            }

            _logger.LogDebug("WholesalerQueryService found wholesaler with id {1}. So, it can proceed with the query", wholesalerId);

            var beer = await _unitOfWork.QueryInventoryBeer.GetBeerInfoByCondition(b => b.WholesalerId == wholesalerId && b.BeerId == beerId);

            if (!beer.Any())
            {
                _logger.LogInfo("WholesalerQueryService was trying to retrieve a wholesaler beer but did not find the specified beer with id {1}", beerId);
                return new BeerNotSoldByWholesaler(wholesalerId, beerId);
            }

            _logger.LogDebug("WholesalerQueryService successfully retrieved beer with id {1} sold by the wholesaler with id {2}", beerId, wholesalerId);

            return _mapper.Map<GetInventoryBeerDto>(beer.First());
        }

        public async Task<OneOf<GetWholesalerDto, IError>> GetWholesalerById(int wholesalerId)
        {
            var wholesaler = await _unitOfWork.QueryWholesaler.GetByCondition(w => w.WholesalerId == wholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("WholesalerQueryService was trying to retrieve information from the wholesaler with id {1}. But such id does not correspond to any existing wholesaler", wholesalerId);
                return new WholesalerNotFound(wholesalerId);
            }

            return _mapper.Map<GetWholesalerDto>(wholesaler.First());

        }
    }
}
