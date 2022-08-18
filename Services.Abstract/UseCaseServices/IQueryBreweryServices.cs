using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
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
        /// returns IError with Number 404.
        /// </summary>
        public Task<OneOf<BreweryDto, IError>> GetById(int breweryId);
    }
}
