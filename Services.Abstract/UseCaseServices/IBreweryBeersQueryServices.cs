using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
{
    public interface IBreweryBeersQueryServices
    {
        /// <summary>
        /// Returns all the beers produced by the brewery identified by breweryId.
        /// If the brewery does not exist, it returns an BreweryNotFound error (Number 404).
        /// </summary>
        /// <param name="BreweryId">Id of a brewery</param>
        public Task<OneOf<IEnumerable<BeerDto>, IError>> GetAllBeers(int breweryId);

        /// <summary>
        /// Returns the beer produced by the brewery identified by BreweryId, whose id is BeerId.
        /// If the brewery does not exist, it returns a BreweryNotFound error (Number 404).
        /// If the beer does not exists, it returns a BreweryBeerNotFound error (Number 404).
        public Task<OneOf<BeerDto, IError>>
            GetBeerById(int breweryId, int beerId);
    }
}
