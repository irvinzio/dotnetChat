using AutoMapper;
using DotnetChat.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetChat.Service.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddRepositoryMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Mapping.DataModelMappingProfile));
            return services;
        }
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddRepositoryMappings();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IAutheticationService, AuthenticationService>();
            return services;
        }
    }
}
