using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
{
    public interface IWholesalerQueryServices
    {
        /// <summary>
        /// Returns all the wholesalers 
        /// </summary>
        public Task<IEnumerable<GetWholesalerDto>> GetAllWholesalers();

        /// <summary>
        /// Returns wholesaler with specified id.
        /// If there is not any wholesaler with id wholesalerId, it returns WholesalerNotFound
        /// </summary>
        public Task<OneOf<GetWholesalerDto, IError>> GetWholesalerById(int wholesalerId);

        /// <summary>
        /// Returns all beers associated to the wholesaler whose id is wholesalerId.
        /// If there is not any wholesaler assigned to id wholesalerId, it returns an WholesalerNotFound error (Number: 404).
        /// </summary>
        /// <param name="wholesalerId"></param>
        public Task<OneOf<IEnumerable<GetInventoryBeerDto>, IError>> GetAllWholesalerBeers(int wholesalerId);

        /// <summary>
        /// Returns the wholesaler-owned beer with id beerId.
        /// If there is no any wholesaler assigned to id wholesalerId, it returns a WholesalerNotFound error (Number: 404).
        /// If there is no any beer with id beerId assigned to the wholesaler with id wholesalerId, it returns a BeerNotSoldByWholesaler error (Number: 404)
        /// </summary>
        /// <param name="wholesalerId"></param>
        /// <param name="beerId"></param>
        public Task<OneOf<GetInventoryBeerDto, IError>> GetWholesalerBeerById(int wholesalerId, int beerId);
    }
}
