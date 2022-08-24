using Mapster;
using MapsterMapper;
using Services.Mappings;

namespace BeerApi.Test.Helpers
{
    public static class MapperInstance
    {
        public static IMapper Get()
        {
            TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);

            var config = TypeAdapterConfig.GlobalSettings;

            //config custom mappings

            new BeerDtoMappingConfig().Register(config);

            new SaleDtoMappingConfig().Register(config);

            new InventoryBeerDtoMappingConfig().Register(config);

            new QuoteMappings().Register(config);

            var mapper = new Mapper();

            return mapper;
        }
    }
}
