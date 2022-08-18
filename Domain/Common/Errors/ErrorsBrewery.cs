using Domain.Common.Errors.Base;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common.Errors
{
    public sealed record BreweryNotFound(int breweryId) : NotFound, IError
    {
        public new string Message => $"Brewery with id {breweryId} does not exist";

        public new string Code => "Brewery.NotFound";
    }
}
