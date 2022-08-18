using Domain.Repositories;
using MapsterMapper;
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

        public ServicesWrapper(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public ICommandBreweryBeersServices ChangeBreweryBeers => throw new NotImplementedException();

        public IQueryBreweryBeersServices QueryBreweryBeers => new QueryBreweryBeersServices(_unitOfWork, _mapper);

        public IQueryBreweryServices QueryBrewery => new QueryBreweryServices(_unitOfWork, _mapper);
    }
}
