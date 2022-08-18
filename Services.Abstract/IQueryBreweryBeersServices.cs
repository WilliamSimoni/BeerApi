using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using OneOf;
using System.Collections.Generic;

namespace Services.Abstract
{
    public interface IQueryBreweryBeersServices
    {
        /// <summary>
        /// Returns all the beers produced by the brewery identified by breweryId.
        /// If the brewery does not exist, it returns a NotFound error.
        /// </summary>
        /// <param name="BreweryId">Id of a brewery</param>
        public Task<OneOf<IEnumerable<BeerDto>, NotFoundError>> GetAllBeers(int breweryId);

        /// <summary>
        /// Returns the beer produced by the brewery identified by BreweryId, whose id is BeerId.
        /// If the brewery does not exist, it returns a NotFound error.
        /// If the beer does not exists, it returns a NotFound error.
        /// <param name="BreweryId"></param>
        /// <param name="BeerId"></param>
        public Task<OneOf<BeerDto, NotFoundError>> 
            GetBeerById(int BreweryId, int BeerId);
    }
}
