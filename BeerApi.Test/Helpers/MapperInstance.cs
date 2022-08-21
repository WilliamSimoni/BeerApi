﻿using Mapster;
using MapsterMapper;
using Services.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeerApi.Test.Helpers
{
    public static class MapperInstance
    {
        public static IMapper Get()
        {
            TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);

            var config = TypeAdapterConfig.GlobalSettings;

            new BeerDtoMappingConfig().Register(config);

            new SaleDtoMappingConfig().Register(config);

            var mapper = new Mapper();

            return mapper;
        }
    }
}