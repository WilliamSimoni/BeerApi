using Domain.Common.Errors.Base;

namespace Domain.Common.Errors
{

    //NOT FOUND errors

    public sealed record WholesalerNotFound(int wholesalerId) : NotFoundError, IError
    {
        public new string Message => $"Wholesaler with specified id does not exist. [wholesalerId: {wholesalerId}]";

        public new string Code => "Wholesaler.NotFound";
    }
}
