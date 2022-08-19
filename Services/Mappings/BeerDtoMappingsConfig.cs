using Contracts.Dtos;
using Domain.Entities;
using Mapster;

namespace Services.Mappings
{
    internal class BeerDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ForCreationBeerDto, Beer>()
                .Map(dest => dest.SellingPriceToClients, src => Math.Round(src.SellingPriceToClients, 2))
                .Map(dest => dest.SellingPriceToWholesalers, src => Math.Round(src.SellingPriceToWholesalers, 2));
        }
    }
}