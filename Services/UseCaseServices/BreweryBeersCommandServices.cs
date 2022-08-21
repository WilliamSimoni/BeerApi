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
    public class BreweryBeersCommandServices : IBreweryBeersCommandServices
    {

        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public BreweryBeersCommandServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<BeerDto, IError>> AddBeerToBrewery(int breweryId, ForCreationBeerDto creationBeerDto)
        {
            //check brewery existence
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId);

            if (!brewery.Any())
            {
                _logger.LogInfo("BreweryBeersCommandService tried to add a beer, but the id {1} did not correspond to any existing brewery", breweryId);
                return new BreweryNotFound(breweryId);
            }

            _logger.LogDebug("BreweryBeersCommandService found a brewery with id {1}. So, it can proceed with the addition of a beer", breweryId);

            var newBeer = _mapper.Map<Beer>(creationBeerDto);

            _logger.LogDebug("BreweryBeersCommandService mapped ForCreationBeerDto to BeerDto");

            //check if there exists a beer produced by the brewery with id breweryId
            //whose name is creationBeerDto.Name
            var beers = await _unitOfWork.QueryBeer
                .GetByCondition(b =>
                b.BreweryId == breweryId &&
                b.InProduction == true &&
                b.Name == newBeer.Name
                );

            if (beers.Any())
            {
                _logger.LogInfo("BreweryBeersCommandService tried to add a beer, but the name {1} is already assigned to an existing brewery", newBeer.Name);
                return new BreweryBeerConflict(newBeer.Name, breweryId);
            }

            _logger.LogDebug("BreweryBeersCommandService did not find breweries already associated with the name {1}. So, it can proceed with the addition of a beer", newBeer.Name);

            newBeer.BreweryId = breweryId;

            _unitOfWork.ChangeBeer.Add(newBeer);
            int result = await _unitOfWork.SaveAsync();

            if (result == 1)
            {
                _logger.LogError("BreweryBeersCommandService was not able to add a new beer to the repository");
                return new BeerInsertionInternalError();
            }

            _logger.LogDebug("BreweryCommandService added a new beer with id {1} to the repository", newBeer.BeerId);


            return _mapper.Map<BeerDto>(newBeer);
        }

        public async Task<IError> RemoveBeerFromBrewery(int breweryId, int beerId)
        {
            //check brewery existence
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId);

            if (!brewery.Any())
            {
                _logger.LogWarn("Brewery id is not valid");
                return new BreweryNotFound(breweryId);
            }

            //check if there exists a beer with id beerId produced by the brewery with id breweryId
            var beers = await _unitOfWork.QueryBeer
                .GetByCondition(b =>
                b.BreweryId == breweryId &&
                b.BeerId == beerId &&
                b.InProduction == true
                );

            if (!beers.Any())
            {
                _logger.LogInfo("");
                return new BreweryBeerNotFound(beerId, breweryId);
            }

            //set deletion fields (to soft delete the beer)
            var beer = beers.First();

            beer.InProduction = false;
            beer.OutOfProductionDate = DateTime.UtcNow;

            _unitOfWork.ChangeBeer.Update(beer);

            await _unitOfWork.SaveAsync();

            return null;
        }
    }
}
