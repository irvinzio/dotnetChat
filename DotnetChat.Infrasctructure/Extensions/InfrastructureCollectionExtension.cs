using DotnetChat.Core.Interfaces.Repositories;
using DotnetChat.Data.Context;
using DotnetChat.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    }
}
