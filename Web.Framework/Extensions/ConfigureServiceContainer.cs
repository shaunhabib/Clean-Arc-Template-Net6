﻿using AutoMapper;
using Core.Application;
using Core.Domain.Shared.Contacts;
using Infrastructure.Persistence.Extensions;
using Infrastructure.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Web.Framework.Services;

namespace Web.Framework.Extensions
{
    public static class ConfigureServiceContainer
    {
        public static void AddAutoMapper(this IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(cfg =>
                    cfg.AddMaps(new[] {
                        "Web.Framework",
                        "Core.Application",
                        "Core.Application.Contracts",
                        "Infrastructure.Persistence"
                    })
                );
            IMapper mapper = mappingConfig.CreateMapper();
            serviceCollection.AddSingleton(mapper);
        }

        public static void AddFramework(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddPersistenceDbContext(configuration);
            services.AddPersistenceRepositories();

            services.AddApplicationLayer();
            //services.AddRabbitMqInfrastructure();
            services.AddSharedInfrastructure();

            services.AddHttpContextAccessor();
            services.AddTransient<IAuthenticatedUser, AuthenticatedUser>();
            services.AddTransient<IDateTimeService, DateTimeService>();

        }

        public static void AddApiVersioningExtension(this IServiceCollection services)
        {

            #region API Versioning
            // Add API Versioning to the Project
            services.AddApiVersioning(config =>
            {
                // Specify the default API Version as 1.0
                config.DefaultApiVersion = new ApiVersion(1, 0);
                // If the client hasn't specified the API version in the request, use the default API version number 
                config.AssumeDefaultVersionWhenUnspecified = true;
                // Advertise the API versions supported for the particular endpoint
                config.ReportApiVersions = true;
            });
            #endregion
        }
    }
}
