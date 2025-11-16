using Microsoft.Extensions.DependencyInjection;

namespace SriDurgaHariHaraBackend.Config
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddSriDurgaServices(this IServiceCollection services)
        {
            // Add DI registrations here
            return services;
        }
    }
}