using DatingApp_API.Data;
using DatingApp_API.Interfaces;
using DatingApp_API.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DatingApp_API.Extentions
{
    public static class ApplicationServiceExtentions
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<ITokenService, TokenService>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var migrationsAssembly = typeof(DataContext).GetTypeInfo().Assembly.GetName().Name;
            services.AddDbContext<DataContext>(options =>
               options.UseSqlServer(connectionString, b => b.MigrationsAssembly(migrationsAssembly)), ServiceLifetime.Transient);
            return services;
        }
    }
}
