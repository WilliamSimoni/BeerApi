using Contracts.Dtos;
using Domain.Common.Errors;
using OneOf;

namespace Services.Abstract
{
    public interface IQueryBreweryServices
    {
        /// <summary>
        /// Returns all the breweries saved in the database
        /// </summary>
        public Task<IEnumerable<BreweryDto>> GetAll();

        /// <summary>
        /// Returns the brewery with the id specified as a parameter.
        /// If there is not any brewery with the specified Id, it
        /// returns a BreweryNotFound IError object.
        /// </summary>
        public Task<OneOf<BreweryDto, BreweryNotFound>> GetById(int id);
    }
}
