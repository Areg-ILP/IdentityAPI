using FluentValidation.AspNetCore;
using Identity.API.Middlewares;
using Identity.API.Utilities.Extentions;
using Identity.Infastructure.Application.Utilities.AutoMapper;
using Identity.Infastructure.Application.Utilities.Models;
using Identity.Infastructure.Contexts;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var section = "JWTConfigs";
            services.Configure<JWTConfigs>(options => Configuration.GetSection(section).Bind(options));

            var jwtConfigs = Configuration.GetSection(section).Get<JWTConfigs>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = jwtConfigs.RequireHttpsMetadata;
                        options.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidateIssuer = jwtConfigs.ValidateIssuer,
                            ValidIssuer = jwtConfigs.ValidIssuer,
                            ValidAudience = jwtConfigs.ValidAudience,
                            ValidateLifetime = jwtConfigs.ValidateLifetime,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigs.Key)),
                            ValidateIssuerSigningKey = jwtConfigs.ValidateIssuerSigningKey,
                        };
                    });

            services.AddControllers()
                    .AddFluentValidation(fv =>
                    {
                        fv.RegisterValidatorsFromAssemblyContaining<Startup>();
                    });

            services.AddSwaggerWithSecurityRequirement();

            services.AddDbContext<IdentityDbContext>(
                options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("IdentityDbContext"));
                    options.EnableSensitiveDataLogging();
                });

            services.AddAutoMapper(typeof(MapperProfile));
            services.AddCustomServices();
            services.AddMediatR(typeof(Startup).Assembly);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseMiddleware<LoggerMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
