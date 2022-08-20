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

        private IBreweryBeersCommandServices? _breweryBeersCommandServices;
        private IBreweryBeersQueryServices? _breweryBeersQueryServices;
        private IBreweryQueryServices? _breweryQueryServices;
        private ISaleCommandServices? _saleCommandServices;
        private ISaleQueryServices? _saleQueryServices;
        private IWholesalerCommandServices? _wholesalerCommandServices;
        private IWholesalerQueryServices? _wholesalerQueryServices;

        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;

        public ServicesWrapper(IUnitOfWork unitOfWork, IMapper mapper, ILoggerManager loggerManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = loggerManager;
        }

        public IBreweryBeersCommandServices ChangeBreweryBeers
        {
            get
            {
                _breweryBeersCommandServices ??= new BreweryBeersCommandServices(
                    _logger,
                    _unitOfWork,
                    _mapper);
                return _breweryBeersCommandServices;
            }
        }

        public IBreweryBeersQueryServices QueryBreweryBeers
        {
            get
            {
                _breweryBeersQueryServices ??= new BreweryBeersQueryServices(
                    _logger,
                    _unitOfWork,
                    _mapper);
                return _breweryBeersQueryServices;
            }
        }


        public IBreweryQueryServices QueryBrewery
        {
            get
            {
                _breweryQueryServices ??= new BreweryQueryServices(
                    _logger,
                    _unitOfWork,
                    _mapper);
                return _breweryQueryServices;
            }
        }

        public ISaleQueryServices QuerySale
        {
            get
            {
                _saleQueryServices ??= new SaleQueryServices(
                    _logger,
                    _unitOfWork,
                    _mapper
                    );
                return _saleQueryServices;
            }
        }

        public ISaleCommandServices ChangeSale
        {
            get
            {
                _saleCommandServices ??= new SaleCommandServices(
                    _logger,
                    _unitOfWork,
                    _mapper
                    );
                return _saleCommandServices;
            }
        }

        public IWholesalerQueryServices QueryWholesaler
        {
            get
            {
                _wholesalerQueryServices = new WholesalerQueryServices(
                    _logger,
                    _unitOfWork,
                    _mapper);
                return _wholesalerQueryServices;
            }
        }

        public IWholesalerCommandServices ChangeWholesaler
        {
            get
            {
                _wholesalerCommandServices = new WholesalerCommandServices(
                    _logger,
                    _unitOfWork,
                    _mapper);
                return _wholesalerCommandServices;
            }
        }
    }
}
