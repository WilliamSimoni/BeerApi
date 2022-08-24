using Domain.Common.Errors.Base;

namespace Domain.Common.Errors
{
    //NOT FOUND errors

    public sealed record BreweryNotFound(int breweryId) : NotFoundError, IError
    {
        public new string Message => $"Brewery with specified id does not exist. [breweryId: {breweryId}]";

        public new string Code => "Brewery.NotFound";
    }
}
