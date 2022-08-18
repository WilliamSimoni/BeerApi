using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OneOf;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    internal class QueryBreweryServices : IQueryBreweryServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public QueryBreweryServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        public async Task<IEnumerable<BreweryDto>> GetAll()
        {
            var breweries = _unitOfWork.QueryBrewery.GetAll();

            var breweriesList = await breweries.ToListAsync();

            return _mapper.Map<BreweryDto[]>(breweriesList);
        }

        public async Task<OneOf<BreweryDto, IError>> GetById(int breweryId)
        {
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId).
                FirstOrDefaultAsync();

            if (brewery is null)
                return new BreweryNotFound(breweryId);

            return _mapper.Map<BreweryDto>(brewery);
        }
    }
}
