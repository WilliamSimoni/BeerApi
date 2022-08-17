using Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class ServicesWrapper : IServicesWrapper
    {
        public ICommandBreweryBeersServices ChangeBreweryBeers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQueryBreweryBeersServices QueryBreweryBeers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IQueryBreweryServices QueryBrewery { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
