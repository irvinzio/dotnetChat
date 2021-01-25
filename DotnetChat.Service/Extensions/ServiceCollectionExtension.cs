using DotnetChat.Core.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace DotnetChat.Service.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IChatService, ChatService>();
            return services;
        }
    }
}
