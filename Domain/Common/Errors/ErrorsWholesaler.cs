using Domain.Common.Errors.Base;

namespace Domain.Common.Errors
{
        public sealed record WholesalerNotFound(int wholesalerId) : NotFoundError, IError
        {
            public new string Message => $"Wholesaler with specified id does not exist. [wholesalerId: {wholesalerId}]";

            public new string Code => "Wholesaler.NotFound";
        }

        public sealed record BadWholesalerId(int wholesalerId) : BadRequestError, IError
        {
            public new string Message => $"Wholesaler with specified id does not exist. [wholesalerId: {wholesalerId}]";

            public new string Code => "Wholesaler.BadRequest";
        }
}
