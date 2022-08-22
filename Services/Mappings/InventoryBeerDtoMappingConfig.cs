using Contracts.Dtos;
using Domain.Entities;
using Mapster;

namespace Services.Mappings
{
    public class InventoryBeerDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<InventoryBeer, GetInventoryBeerDto>()
                .Map(dest => dest.BeerId, src => src.Beer.BeerId)
                .Map(dest => dest.BreweryId, src => src.Beer.BreweryId)
                .Map(dest => dest.Name, src => src.Beer.Name)
                .Map(dest => dest.AlcoholContent, src => src.Beer.AlcoholContent)
                .Map(dest => dest.SellingPrice, src => src.Beer.SellingPriceToClients);
        }
    }
}