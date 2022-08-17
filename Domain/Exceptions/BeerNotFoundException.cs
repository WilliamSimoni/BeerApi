using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions
{
    public sealed class BeerNotFoundException : NotFoundException
    {
        public BeerNotFoundException(int BeerId) : base($"Beer with id {BeerId} does not exist")
        {
        }
    }
}
