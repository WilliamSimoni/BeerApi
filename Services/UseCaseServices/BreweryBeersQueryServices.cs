using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
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
                _logger.LogWarn("TNot valid Id]", breweryId);
                return new BreweryNotFound(breweryId);
            }

            _logger.LogDebug("Retrieved brewery with specified id. [breweryId = {@breweryId}]", breweryId);

            //get beers associated with breweryId
            var beers = await _unitOfWork.QueryBeer
                .GetByCondition(b => b.BreweryId == breweryId && b.InProduction == true);

            _logger.LogDebug("Retrieved all beers produced by the specified brewery. [breweryId = {1}]", breweryId);

            return _mapper.Map<BeerDto[]>(beers);
        }

        public async Task<OneOf<BeerDto, IError>> GetBeerById(int breweryId, int beerId)
        {
            //check brewery existence
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId);

            if (!brewery.Any())
                return new BreweryNotFound(breweryId);

            //get beers associated with breweryId
            var beer = await _unitOfWork.QueryBeer
                .GetByCondition(b => b.BreweryId == breweryId && b.BeerId == beerId && b.InProduction == true);

            if (!beer.Any())
                return new BreweryBeerNotFound(beerId, breweryId);

            return _mapper.Map<BeerDto>(beer.ElementAt(0));
        }
    }
}
