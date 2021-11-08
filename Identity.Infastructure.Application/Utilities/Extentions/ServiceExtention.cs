using Identity.Domain.RepositoryAbstraction;
using Identity.Infastructure.Application.Queries.QueriesAbstraction;
using Identity.Infastructure.Application.Queries.QueriesImplementations;
using Identity.Infastructure.Application.Services.ServiceAbstractions;
using Identity.Infastructure.Application.Services.ServiceImplementations;
using Identity.Infastructure.RepositoryImplementations;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Infastructure.Application.Utilities.Extentions
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
    }
}
