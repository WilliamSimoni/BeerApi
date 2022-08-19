using Contracts.Dtos;
using Domain.Common.Errors.Base;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using OneOf;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    internal class QuerySaleServices : IQuerySaleServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public QuerySaleServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public Task<IEnumerable<GetSaleDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<GetSaleDto, IError>> GetBeerInvolvedInSale(int saleId)
        {
            throw new NotImplementedException();
        }

        public Task<OneOf<GetSaleDto, IError>> GetById(int saleId)
        {
            throw new NotImplementedException();
        }
    }
}