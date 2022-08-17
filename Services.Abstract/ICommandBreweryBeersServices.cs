using Contracts.Dtos;

namespace Services.Abstract
{
    public interface ICommandBreweryBeersServices
    {
        /// <summary>
        /// Add a beer defined with a ForCreationBeerDto object to the brewery identified by the BreweryId
        /// </summary>
        /// <param name="BreweryId">Id of the brewery to which add the beer</param>
        /// <param name="ForCreationbeerDto">Beer to add</param>
        /// <returns>Returns the newly added Beer</returns>
        public Task<BeerDto> AddBeerToBrewery(int BreweryId, ForCreationBeerDto forCreationBeerDto);

        /// <summary>
        /// Removes the beer with id equals to BeerId from the brewery identified by the BreweryId
        /// </summary>
        /// <param name="BreweryId">Id of the brewery from which remove the beer</param>
        /// <param name="BeerId">Id of the beer to remove</param>
        public Task RemoveBeerFromBrewery(int BreweryId, int BeerId);
    }
}
