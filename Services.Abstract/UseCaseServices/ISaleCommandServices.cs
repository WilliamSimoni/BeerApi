using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
{
    public interface ISaleCommandServices
    {
        /// <summary>
        /// Add a new sale to the database.
        /// If the specified beer does not exist, it returns a BeerNotFound error (Number: 404)
        /// If the specified wholesaler does not exist, it returns a WholesalerNotFound error (Number: 404)
        /// </summary>
        public Task<OneOf<CreatedSaleDto, IError>> addSale(ForCreationSaleDto creationSaleDto);
    }
}
