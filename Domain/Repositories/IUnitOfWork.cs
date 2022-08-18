using Domain.Repositories.Specialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUnitOfWork
    {
        public IBeerCommandRepository ChangeBeer { get; }
        public IBeerQueryRepository QueryBeer { get; }
        public IBreweryCommandRepository ChangeBrewery { get; }
        public IBreweryQueryRepository QueryBrewwery { get; }

        /// <summary>
        /// Asynchronously saves the latest changes in the database
        /// </summary>
        public Task saveAsync();
    }
}
