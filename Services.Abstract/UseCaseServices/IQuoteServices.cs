using Contracts.Dtos;
using Domain.Common.Errors.Base;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract.UseCaseServices
{
    public interface IQuoteServices
    {
        /// <summary>
        /// Returns a summary of the requested quote.
        /// If the specified wholesaler does not exist, it returns a WholesalerNotFound error (Number 404)
        /// If the wholesaler does not sell a beer, it returns a BeerNotSoldByWholesaler error (Number 404)
        /// If there are not enough units in the wholesaler stock, it returns a QuantityOverUnitsInStock error (Number 400)
        /// </summary>
        /// <param name="quoteRequest"></param>
        /// <returns></returns>
        public Task<OneOf<QuoteSummaryDto, IError>> GetQuote(QuoteRequestDto quoteRequest);
    }
}
