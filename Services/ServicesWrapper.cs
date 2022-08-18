using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using Microsoft.Extensions.Logging;
using Services.Abstract;
using Services.Abstract.UseCaseServices;
using Services.UseCaseServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public ICommandBreweryBeersServices ChangeBreweryBeers => throw new NotImplementedException();

        public IQueryBreweryBeersServices QueryBreweryBeers => new QueryBreweryBeersServices(
            _logger,
            _unitOfWork, 
            _mapper);

        public IQueryBreweryServices QueryBrewery => new QueryBreweryServices(
            _logger,
            _unitOfWork, 
            _mapper);
    }
}
