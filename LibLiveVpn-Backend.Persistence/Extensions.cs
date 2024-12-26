using LibLiveVpn_Backend.Application.Interfaces.Repositories;
using LibLiveVpn_Backend.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibLiveVpn_Backend.Persistence
{
    public static class Extensions
    {
        public static IServiceCollection AddPersistenceDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var databaseConnectionSection = configuration.GetSection(DbConfiguration.Section);
            var databaseConfiguration = new DbConfiguration();

            databaseConfiguration.Host = databaseConnectionSection["Host"] ?? throw new ArgumentNullException($"{DbConfiguration.Section}:Host not exists");
            if (!int.TryParse(databaseConnectionSection["Port"], out int port))
            {
                throw new ArgumentNullException("Database:Port not exists");
            }
            databaseConfiguration.Port = port;
            databaseConfiguration.Database = databaseConnectionSection["Database"] ?? throw new ArgumentNullException($"{DbConfiguration.Section}:Database not exists");
            databaseConfiguration.Username = databaseConnectionSection["Username"] ?? throw new ArgumentNullException($"{DbConfiguration.Section}:Username not exists");
            databaseConfiguration.Password = databaseConnectionSection["Password"] ?? throw new ArgumentNullException($"{DbConfiguration.Section}:Password not exists");


            services.AddDbContext<ApplicationDbContext>(opt =>
            {
                opt.UseNpgsql($"Host={databaseConfiguration.Host};Port={databaseConfiguration.Port};Database={databaseConfiguration.Database};Username={databaseConfiguration.Username};Password={databaseConfiguration.Password}", options =>
                {
                    options.EnableRetryOnFailure();
                });
            });

            services.AddScoped<IServerRepository, ServerRepositoryEfCore>();
            services.AddScoped<IUserRepository, UserRepositoryEfCore>();

            return services;
        }
    }
}
