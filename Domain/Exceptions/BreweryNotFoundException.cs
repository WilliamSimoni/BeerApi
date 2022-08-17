using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BreweryNotFoundException : NotFoundException
    {
        public BreweryNotFoundException(int BreweryId) : base($"Brewery with id {BreweryId} does not exist")
        {

        }
    }
}
