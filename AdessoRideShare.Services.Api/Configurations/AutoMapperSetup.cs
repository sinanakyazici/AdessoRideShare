using System;
using AdessoRideShare.Application.AutoMapper;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace AdessoRideShare.Services.Api.Configurations
{
  public static class AutoMapperSetup
  {
      public static void AddAutoMapperSetup(this IServiceCollection services)
      {
          if (services == null) throw new ArgumentNullException(nameof(services));

          services.AddAutoMapper();
          AutoMapperConfig.RegisterMappings();
      }
  }
}