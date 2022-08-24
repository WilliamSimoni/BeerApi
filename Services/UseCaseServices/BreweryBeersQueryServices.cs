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
    public class BreweryBeersQueryServices : IBreweryBeersQueryServices
    {

        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public BreweryBeersQueryServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<IEnumerable<BeerDto>, IError>> GetAllBeers(int breweryId)
        {
            //check brewery existence
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId);

            if (!brewery.Any())
            {
                _logger.LogInfo("BreweryBeersCommandService tried to retrieve all the beers produced by a brewery, but the id {1} did not correspond to any existing brewery", breweryId);
                return new BreweryNotFound(breweryId);
            }

            _logger.LogDebug("BreweryBeersCommandService found a brewery with id {1}. So, it can proceed with the retrieval of the beers produced by that brewery", breweryId);

            //get beers associated with breweryId
            var beers = await _unitOfWork.QueryBeer
                .GetByCondition(b => b.BreweryId == breweryId && b.InProduction == true);

            _logger.LogDebug("BreweryBeersCommandService retrieved all the beers produced by the brewery with id {1}", breweryId);

            return _mapper.Map<BeerDto[]>(beers);
        }

        public async Task<OneOf<BeerDto, IError>> GetBeerById(int breweryId, int beerId)
        {
            //check brewery existence
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId);

            if (!brewery.Any())
            {
                _logger.LogInfo("BreweryBeersCommandService tried to retrieve a beer produced by a brewery, but the id {1} did not correspond to any existing brewery", breweryId);
                return new BreweryNotFound(breweryId);
            }

            _logger.LogDebug("BreweryBeersCommandService found a brewery with id {1}. So, it can proceed with the retrieval of the beer with id {2}", breweryId, beerId);

            //get beers associated with breweryId
            var beer = await _unitOfWork.QueryBeer
                .GetByCondition(b => b.BreweryId == breweryId && b.BeerId == beerId && b.InProduction == true);

            if (!beer.Any())
            {
                _logger.LogInfo("BreweryBeersCommandService tried to retrieve the beer with id {1} produced by the brewery with id {2}, but it did not find anything", beerId, breweryId);
                return new BreweryBeerNotFound(beerId, breweryId);
            }

            _logger.LogDebug("BreweryBeersCommandService retrieved the beer with id {1} that the brewery with id {2} produces", beerId, breweryId);

            return _mapper.Map<BeerDto>(beer.ElementAt(0));
        }
    }
}
