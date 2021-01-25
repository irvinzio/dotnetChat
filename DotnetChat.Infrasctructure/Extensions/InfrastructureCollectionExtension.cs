using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Data.Context;
using DotnetChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace DotnetChat.Infrasctructure.Extensions
{
    public static class InfrastructureCollectionExtension
    {
        public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddDbContext<DotnetChatContext>(item => {
                item.UseSqlServer(Configuration.GetSection("Database").GetSection("ConnectionString").Value);
            });

            return services;
        }
        public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            return services;
        }
        
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Dotnet Chat API",
                    Description = "API to interact with dotnetChat",
                    Contact = new OpenApiContact
                    {
                        Name = "Irving Ramirez",
                        Email = "Irvinzio.ram@gmail.com",
                        Url = new Uri("https://www.linkedin.com/in/irving-ramirez-847395a5/"),
                    }
                });
            });
            return services;
        }
    }
}
