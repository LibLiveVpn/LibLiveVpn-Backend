using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace LibLiveVpn_Backend.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>();

            services.AddScoped<IServerRepository, ServerRepositoryEfCore>();
            services.AddScoped<IUserRepository, UserRepositoryEfCore>();

            return services;
        }
    }
}
