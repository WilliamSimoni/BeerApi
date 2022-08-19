using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors.Base
{
    public abstract record NotFoundError: IError
    {
        public string Message => "Generic Not Found";

        public string Code => "Generic.NotFound";

        public int Number => 404;
    }

    public abstract record ConflictError : IError
    {
        public string Message => "Generic Conflict";

        public string Code => "Generic.Conflict";

        public int Number => 409;
    }
}
