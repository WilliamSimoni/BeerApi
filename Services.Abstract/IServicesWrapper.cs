using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstract
{
    public interface IServicesWrapper
    {
        public ICommandBreweryBeersServices ChangeBreweryBeers { get; set; }
        public IQueryBreweryBeersServices QueryBreweryBeers { get; set; }
        public IQueryBreweryServices QueryBrewery { get; set; }
    }
}
