using Domain.Common.Errors.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{

    //NOT FOUND errors
    public sealed record SaleNotFound(int saleId) : NotFoundError, IError
    {
        public new string Message => $"Sale with specified id does not exist. [saleId: {saleId}]";

        public new string Code => "Sale.NotFound";
    }

    //INTERNAL SERVER errors

    public sealed record SaleInsertionInternalError() : InternalError, IError
    {
        public new string Message => $"Due to an internal error, it was impossible to add a new sale to the database";

        public new string Code => "Sale.InsertionInternalError";
    }
}
