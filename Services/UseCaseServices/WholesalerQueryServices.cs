using Domain.Logger;
using Domain.Repositories;
using Domain.Repositories.Specialization;
using MapsterMapper;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    public class WholesalerQueryServices : IWholesalerQueryServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public WholesalerQueryServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
