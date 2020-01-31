using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using HealthCatalyst.Assessment.Domain.Models;
using HealthCatalyst.Assessment.API.Models.Team;

namespace HealthCatalyst.Assessment.API
{
    public static class MappingConfig
    {
        #region Unity Container
        private static Lazy<IConfigurationProvider> mapper =
          new Lazy<IConfigurationProvider>(() =>
          {
              var mapper = MappingConfig.Initialize();
              return mapper;
          });

        /// <summary>
        /// Configured mapping configuration.
        /// </summary>
        public static IConfigurationProvider Mapper => mapper.Value;
        #endregion


        public static bool IsInitialized;


        public static IConfigurationProvider Initialize()
        {
            if (MappingConfig.IsInitialized)
                return mapper.Value;

            var config = new AutoMapper.MapperConfiguration(MappingConfig.BuildMappings);
            MappingConfig.IsInitialized = true;
            return config;
        }

        private static void BuildMappings(IMapperConfigurationExpression cfg)
        {
            cfg.AllowNullCollections = true;


            cfg.CreateMap<CreateTeammateRequest, Teammate>()
                .ForMember(d => d.PrimaryPosition, opt => opt.MapFrom(
                    src => string.IsNullOrEmpty(src.PrimaryPosition) ? null : Enum.Parse(typeof(Position), src.PrimaryPosition, true)
                    )); 
  
            cfg.CreateMap<Teammate, CreateTeammateResponse>();

            cfg.CreateMap<Teammate, GetTeammateResponse>()
                .ForMember(d => d.PrimaryPosition, opt =>  opt.MapFrom(
                    src => src.PrimaryPosition.HasValue ? Enum.GetName(src.PrimaryPosition.GetType(), src.PrimaryPosition.Value) : null));

        }
    }
}