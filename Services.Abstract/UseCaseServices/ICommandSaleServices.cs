using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;

namespace Services.Abstract.UseCaseServices
{
    public interface ICommandSaleServices
    {
        /// <summary>
        /// Add a new sale to the database.
        /// If the specified beer does not exist, it returns an IError with number 400.
        /// If the specified wholesaler does not exist, it returns an IError with number 400.
        /// </summary>
        public Task<OneOf<CreatedSaleDto, IError>> addSale(ForCreationSaleDto creationSaleDto);
    }
}
