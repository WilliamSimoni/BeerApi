using Domain.Logger;
using Domain.Repositories;
using Domain.Repositories.Specialization;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Repositories.Repositories.Specialization;

namespace Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ILoggerManager _logger;
        private readonly AppDbContext _context;

        private IBeerCommandRepository? _beerCommandRepository;
        private IBeerQueryRepository? _beerQueryRepository;
        private IBreweryCommandRepository? _breweryCommandRepository;
        private IBreweryQueryRepository? _breweryQueryRepository;
        private ISaleCommandRepository? _saleCommandRepository;
        private ISaleQueryRepository? _saleQueryRepository;
        private IWholesalerCommandRepository? _wholesalerCommandRepository;
        private IWholesalerQueryRepository? _wholesalerQueryRepository;
        private IInventoryBeerCommandRepository _inventoryBeerCommandRepository;
        private IInventoryBeerQueryRepository _inventoryBeerQueryRepository;

        public UnitOfWork(ILoggerManager logger, AppDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IBeerCommandRepository ChangeBeer
        {
            get
            {
                _beerCommandRepository ??= new BeerCommandRepository(_context);
                return _beerCommandRepository;
            }
        }

        public IBeerQueryRepository QueryBeer
        {
            get
            {
                _beerQueryRepository ??= new BeerQueryRepository(_context);
                return _beerQueryRepository;
            }
        }

        public IBreweryCommandRepository ChangeBrewery
        {
            get
            {
                _breweryCommandRepository ??= new BreweryCommandRepository(_context);
                return _breweryCommandRepository;
            }
        }

        public IBreweryQueryRepository QueryBrewery
        {
            get
            {
                _breweryQueryRepository ??= new BreweryQueryRepository(_context);
                return _breweryQueryRepository;
            }
        }

        public ISaleCommandRepository ChangeSale
        {
            get
            {
                _saleCommandRepository ??= new SaleCommandRepository(_context);
                return _saleCommandRepository;
            }
        }

        public ISaleQueryRepository QuerySale
        {
            get
            {
                _saleQueryRepository ??= new SaleQueryRepository(_context);
                return _saleQueryRepository;
            }
        }

        public IWholesalerCommandRepository ChangeWholesaler
        {
            get
            {
                _wholesalerCommandRepository ??= new WholesalerCommandRepository(_context);
                return _wholesalerCommandRepository;
            }
        }

        public IWholesalerQueryRepository QueryWholesaler
        {
            get
            {
                _wholesalerQueryRepository ??= new WholesalerQueryRepository(_context);
                return _wholesalerQueryRepository;
            }
        }

        public IInventoryBeerCommandRepository ChangeInventoryBeer
        {
            get
            {
                _inventoryBeerCommandRepository ??= new InventoryBeerCommandRepository(_context);
                return _inventoryBeerCommandRepository;
            }
        }

        public IInventoryBeerQueryRepository QueryInventoryBeer
        {
            get
            {
                _inventoryBeerQueryRepository ??= new InventoryBeerQueryRepository(_context);
                return _inventoryBeerQueryRepository;
            }
        }

        public async Task<int> SaveAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            } catch (DbUpdateException e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("{1}", e.InnerException);
                return 1;
            }
            return 0;
        }
    }
}
