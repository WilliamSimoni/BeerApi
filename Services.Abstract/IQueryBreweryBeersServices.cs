using Contracts.Dtos;
using System.Collections.Generic;

namespace Services.Abstract
{
    public interface IQueryBreweryBeersServices
    {
        /// <summary>
        /// Returns all the beers produced by the brewery identified by breweryId.
        /// It assumes that breweryId is the id of an existing brewery. 
        /// If not, it throws a BreweryNotFound exception
        /// </summary>
        /// <param name="BreweryId">Id of a brewery</param>
        public Task<IEnumerable<BeerDto>> GetAllBeers(int breweryId);

        /// <summary>
        /// Returns the beer produced by the brewery identified by BreweryId, whose id is BeerId.
        /// It assumess that breweryId is the id of an existing brewery. If not, it throws a BreweryNotFound exception.
        /// If the beer does not exist, it returns null.
        /// </summary>
        /// <param name="BreweryId"></param>
        /// <param name="BeerId"></param>
        public Task<BeerDto> GetBeerById(int BreweryId, int BeerId);
    }
}
