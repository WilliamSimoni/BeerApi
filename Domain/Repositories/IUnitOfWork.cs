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
        public IBreweryQueryRepository QueryBrewery { get; }
        public ISaleCommandRepository ChangeSale { get; }
        public ISaleQueryRepository QuerySale { get; }
        public IWholesalerCommandRepository ChangeWholesaler { get; }
        public IWholesalerQueryRepository QueryWholesaler { get; }
        public IInventoryBeerCommandRepository ChangeInventoryBeer { get; }
        public IInventoryBeerQueryRepository QueryInventoryBeer { get; }

        /// <summary>
        /// Asynchronously saves the latest changes in the database.
        /// Returns 0 in case of success, 1 in case of errors
        /// </summary>
        public Task<int> SaveAsync();
    }
}
