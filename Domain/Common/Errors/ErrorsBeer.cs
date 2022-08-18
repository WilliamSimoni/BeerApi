using Domain.Common.Errors.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public sealed record BreweryBeerNotFound(int beerId, int breweryId) : NotFoundError, IError
    {
        public new string Message => $"Beer with specified id does not exist for the specified brewery. [beerId: {beerId}, breweryId: {breweryId}]";

        public new string Code => "Brewery.NotFound";
    }
}
