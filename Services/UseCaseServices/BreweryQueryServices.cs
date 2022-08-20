using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    public class BreweryQueryServices : IBreweryQueryServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public BreweryQueryServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork; 
            _mapper = mapper;
        }

        public async Task<IEnumerable<BreweryDto>> GetAll()
        {
            var breweries = await _unitOfWork.QueryBrewery.GetAll();

            return _mapper.Map<BreweryDto[]>(breweries);
        }

        public async Task<OneOf<BreweryDto, IError>> GetById(int breweryId)
        {
            var brewery = await _unitOfWork.QueryBrewery.GetByCondition(b => b.BreweryId == breweryId);

            if (!brewery.Any())
                return new BreweryNotFound(breweryId);

            return _mapper.Map<BreweryDto>(brewery.First());
        }
    }
}
