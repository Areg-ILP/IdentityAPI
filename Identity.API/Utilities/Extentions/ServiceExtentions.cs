﻿using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
using Identity.Infastructure.Application.Queries.QueriesImplementations;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
using Identity.Infastructure.Application.Services.ServiceImplementations;
using Identity.Infastructure.RepositoryImplementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;

namespace Identity.API.Utilities.Extentions
{
    public static class ServiceExtention
    {
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IRoleRepository, RoleRepository>();
            services.AddTransient<IClaimRepository, ClaimRepository>();
            services.AddTransient<ITokenGenerator, TokenGenerator>();
            services.AddTransient<IAuthenticator, Authenticator>();
            services.AddTransient<IRoleQueries, RoleQueries>();

            return services;
        }

        public static IServiceCollection AddSwaggerWithSecurityRequirement(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer",
                    BearerFormat = "JWT"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            return services;
        }
    }
}

