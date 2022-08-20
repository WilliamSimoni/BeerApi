using Domain.Common.Errors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public sealed record SaleNotFound(int saleId) : NotFoundError, IError
    {
        public new string Message => $"Sale with specified id does not exist. [saleId: {saleId}]";

        public new string Code => "Sale.NotFound";
    }
}
