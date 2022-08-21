using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors.Base
{
    public abstract record BadRequestError : IError
    {
        public string Message => "Generic Bad Request";

        public string Code => "Generic.BadRequest";

        public int Number => 400;
    }

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
    public abstract record InternalError : IError
    {
        public string Message => "An error occured in the server";

        public string Code => "Generic.InternalError";

        public int Number => 500;
    }
}
