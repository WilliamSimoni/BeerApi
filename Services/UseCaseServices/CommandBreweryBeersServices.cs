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
    internal class CommandBreweryBeersServices : ICommandBreweryBeersServices
    {

        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public CommandBreweryBeersServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
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
                _logger.LogWarn("Brewery id is not valid");
                return new BreweryNotFound(breweryId);
            }

            //check if there exists a beer produced by the brewery with id breweryId
            //whose name is creationBeerDto.Name
            var beers = await _unitOfWork.QueryBeer
                .GetByCondition(b =>
                b.BreweryId == breweryId &&
                b.InProduction == true &&
                b.Name == creationBeerDto.Name
                );

            if (beers.Any())
            {
                _logger.LogInfo("");
                return new BreweryBeerConflict(creationBeerDto.Name, breweryId);
            }

            //truncate(if needed) prices
            creationBeerDto.SellingPriceToClients = Math.Round(creationBeerDto.SellingPriceToClients, 2);
            creationBeerDto.SellingPriceToWholesalers = Math.Round(creationBeerDto.SellingPriceToClients, 2);

            //map creationBeerDto to beer entity
            var newBeer = _mapper.Map<Beer>(creationBeerDto);

            newBeer.BreweryId = breweryId;

            //Add the beer
            _unitOfWork.ChangeBeer.Add(newBeer);

            await _unitOfWork.saveAsync();

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

            await _unitOfWork.saveAsync();

            return null;
        }
    }
}
