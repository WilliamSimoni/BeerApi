using Domain.Repositories;
using Domain.Repositories.Specialization;
using Repositories.DataContext;
using Repositories.Repositories.Specialization;

namespace Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IBeerCommandRepository ChangeBeer => new BeerCommandRepository(_context);

        public IBeerQueryRepository QueryBeer => new BeerQueryRepository(_context);

        public IBreweryCommandRepository ChangeBrewery => new BreweryCommandRepository(_context);

        public IBreweryQueryRepository QueryBrewery => new BreweryQueryRepository(_context);

        public ISaleCommandRepository ChangeSale => new SaleCommandRepository(_context);

        public ISaleQueryRepository QuerySale => new SaleQueryRepository(_context);

        public IWholesalerCommandRepository ChangeWholesaler => new WholesalerCommandRepository(_context);

        public IWholesalerQueryRepository QueryWholesaler => new WholesalerQueryRepository(_context);

        public async Task saveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
