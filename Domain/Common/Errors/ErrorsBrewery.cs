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

    public sealed record BreweryNotFound(int breweryId) : NotFoundError, IError
    {
        public new string Message => $"Brewery with specified id does not exist. [breweryId: {breweryId}]";

        public new string Code => "Brewery.NotFound";
    }
}
