using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
{
    public interface IWholesalerCommandServices
    {
        /// <summary>
        /// Update beer quantity in wholesaler inventory.
        /// If there is not any wholesaler associated to WholesalerId, it returns a WholesalerNotFound error (Number 404).
        /// If there is not any beer sold by the wholesaler with the specified beerId, it returns a BeerNotSoldByWholesaler error (Number 404).
        /// 
        /// </summary>
        /// <param name="WholesalerId"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        public Task<OneOf<UpdatedInventoryBeerDto, IError>> UpdateQuantity(int wholesalerId, int beerId, ForUpdateInventoryBeerDto inventoryBeerDto);
    }
}
