using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BeerNotFoundInBreweryException : NotFoundException
    {
        public BeerNotFoundInBreweryException(int BreweryId, int BeerId) : base($"Brewery with id {BreweryId} does not produce any beer with id {BeerId}")
        {
        }
    }
}
