using Contracts.Dtos;
using System.Collections.Generic;

namespace Services.Abstract
{
    public interface IQueryBreweryBeersServices
    {
        /// <summary>
        /// Returns all the beers produced by the brewery identified by BreweryId
        /// </summary>
        /// <param name="BreweryId">Id of a brewery</param>
        public Task<IEnumerable<BeerDto>> GetAllBeers(int BreweryId);

        /// <summary>
        /// Returns the beer produced by the brewery identified by BreweryId, whose id is BeerId
        /// </summary>
        /// <param name="BreweryId"></param>
        /// <param name="BeerId"></param>
        public Task<BeerDto> GetBeerById(int BreweryId, int BeerId);
    }
}
