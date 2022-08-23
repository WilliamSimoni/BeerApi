using Contracts.Dtos;
using Domain.Common.Errors;
using Domain.Common.Errors.Base;
using Domain.Logger;
using Domain.Repositories;
using MapsterMapper;
using OneOf;
using Services.Abstract.UseCaseServices;

namespace Services.UseCaseServices
{
    public class WholesalerCommandServices : IWholesalerCommandServices
    {
        private readonly ILoggerManager _logger;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public WholesalerCommandServices(ILoggerManager logger, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<OneOf<UpdatedInventoryBeerDto, IError>> UpdateQuantity(int wholesalerId, int beerId, ForUpdateInventoryBeerDto inventoryBeerDto)
        {
            var wholesaler = await _unitOfWork.QueryWholesaler.GetByCondition(w => w.WholesalerId == wholesalerId);

            if (!wholesaler.Any())
            {
                _logger.LogInfo("WholesalerCommandService was trying to update a beer quantity but did not find the specified wholesaler with id {1}", wholesalerId);
                return new WholesalerNotFound(wholesalerId);            }

            _logger.LogDebug("WholesalerCommandService found wholesaler with id {1}. So, it can proceed with the quantity update", wholesalerId);

            //NOTE: the couple (beerId, wholesalerId) is unique in the InventoryBeer table (since they are an index with unique constraints)
            var inventoryBeer = await _unitOfWork.QueryInventoryBeer.GetByCondition(b => b.BeerId == beerId && b.WholesalerId == wholesalerId);

            if (!inventoryBeer.Any())
            {
                _logger.LogInfo("WholesalerCommandService was trying to update a beer quantity, but beer with id {1} is not sold by wholesaler with id {2}", beerId, wholesalerId);
                return new BeerNotSoldByWholesaler(wholesalerId, beerId);
            }

            _logger.LogDebug("WholesalerCommandService found beer with id {1}. So, it can proceed with the quantity update", wholesalerId);


            var updatedInventoryBeer = inventoryBeer.First();
            updatedInventoryBeer.Quantity = inventoryBeerDto.Quantity;
            _unitOfWork.ChangeInventoryBeer.Update(updatedInventoryBeer);

            await _unitOfWork.SaveAsync();

            _logger.LogDebug("WholesalerCommandService successfully updated the quantity of beer {1} sold by the wholesaler with id {2}", beerId, wholesalerId);

            return _mapper.Map<UpdatedInventoryBeerDto>(updatedInventoryBeer);
        }
    }
}
