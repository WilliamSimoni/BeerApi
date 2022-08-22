using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
{
    public interface ISaleQueryServices
    {
        /// <summary>
        /// Returns all the sales
        /// </summary>
        public Task<IEnumerable<GetSaleDto>> GetAll();

        /// <summary>
        /// Returns the sale with id saleId. 
        /// If there is not any sale associated with that id, it returns a SaleNotFound error (Number 404)
        /// </summary>
        /// <param name="saleId"></param>
        /// <returns></returns>
        public Task<OneOf<GetSaleDto, IError>> GetById(int saleId);

        /// <summary>
        /// Returns the beer associated with the sale with id saleId.
        /// If there is not any sale associated with that id, it returns a SaleNotFound error (Number: 404)
        /// </summary>
        /// <param name="saleId"></param>
        /// <param name="beerId"></param>
        /// <returns></returns>
        public Task<OneOf<GetBeerFromSaleDto, IError>> GetBeerInvolvedInSale(int saleId);

    }
}
