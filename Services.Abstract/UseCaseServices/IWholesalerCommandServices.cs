using Domain.Common.Errors.Base;

namespace Services.Abstract.UseCaseServices
{
    public interface IWholesalerCommandServices
    {
        /// <summary>
        /// Update beer quantity in wholesaler inventory.
        /// If there is not any wholesaler associated to WholesalerId, it returns an IError with number 404.
        /// If the quantity is negative it returns an IError with number 400.
        /// </summary>
        /// <param name="WholesalerId"></param>
        /// <param name="Quantity"></param>
        /// <returns></returns>
        //public Task<OneOf<UpdatedInventoryBeerDto, IError>> UpdateQuantity(int WholesalerId, int Quantity);
    }
}
