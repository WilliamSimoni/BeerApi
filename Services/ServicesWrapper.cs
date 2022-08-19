using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using Services.Abstract;
using Services.Abstract.UseCaseServices;
using Services.UseCaseServices;

namespace Services
{
    public class ServicesWrapper : IServicesWrapper
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ServicesWrapper(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerManager;
        }

        public ICommandBreweryBeersServices ChangeBreweryBeers => new CommandBreweryBeersServices(
            _logger,
            _unitOfWork,
            _mapper);

        public IQueryBreweryBeersServices QueryBreweryBeers => new QueryBreweryBeersServices(
            _logger,
            _unitOfWork, 
            _mapper);

        public IQueryBreweryServices QueryBrewery => new QueryBreweryServices(
            _logger,
            _unitOfWork, 
            _mapper);

        public IQuerySaleServices QuerySale => new QuerySaleServices(
            _logger,
            _unitOfWork,
            _mapper
            );

        public ICommandSaleServices CommandSale => new CommandSaleServices(
            _logger,
            _unitOfWork,
            _mapper
            );
    }
}
