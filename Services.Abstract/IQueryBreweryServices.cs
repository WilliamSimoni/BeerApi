using Contracts.Dtos;

namespace Services.Abstract
{
    public interface IQueryBreweryServices
    {
        /// <summary>
        /// Returns all the breweries saved in the database
        /// </summary>
        public Task<IEnumerable<BreweryDto>> GetAll();

        /// <summary>
        /// Returns the brewery with the id specified as a parameter
        /// </summary>
        public Task<BreweryDto> GetById(int id);
    }
}
