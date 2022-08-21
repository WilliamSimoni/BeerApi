using Contracts.Dtos;
using Domain.Entities;
using Mapster;

namespace Services.Mappings
{
    public class SaleDtoMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<ForCreationSaleDto, Sale>()
                .Map(dest => dest.Total, src => (src.PricePerUnit * src.NumberOfUnits) * ((100 - (decimal)src.Discount) / 100))
                .Map(dest => dest.SaleDate, src => DateTime.Now);
        }
    }
}
