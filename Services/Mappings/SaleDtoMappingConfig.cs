using Contracts.Dtos;
using Domain.Entities;
using Mapster;

namespace Services.Mappings
{
    internal class SaleDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ForCreationSaleDto, Sale>()
                .Map(dest => dest.Total, src => (src.PricePerUnit * src.NumberOfUnits) * ((100 - src.Discount) / 100));
        }
    }
}
