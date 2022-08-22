using Domain.Entities;
using Domain.Repositories.Specialization;
using Microsoft.EntityFrameworkCore;
using Repositories.DataContext;
using Repositories.Repositories.Base;
using System.Linq.Expressions;

namespace Repositories.Repositories.Specialization
{
    public class InventoryBeerQueryRepository : QueryRepositoryBase<InventoryBeer>, IInventoryBeerQueryRepository
    {
        private readonly AppDbContext _context;

        public InventoryBeerQueryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<InventoryBeer>> GetBeerInfoByCondition(Expression<Func<InventoryBeer, Boolean>> condition)
        {
            return await _context.InventoryBeer.Where(condition).Include(b => b.Beer).ToListAsync();
        }
    }

    public class InventoryBeerCommandRepository : CommandRepositoryBase<InventoryBeer>, IInventoryBeerCommandRepository
    {
        public InventoryBeerCommandRepository(AppDbContext context) : base(context)
        {
        }
    }
}
