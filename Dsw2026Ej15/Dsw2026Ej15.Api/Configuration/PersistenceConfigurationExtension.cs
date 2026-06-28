
using Dsw2026Ej15.Data;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15.Api.Configuration
{
    public static class PersistenceConfigurationExtension
    {
        public static IServiceCollection AddAplicationPersistence
            (this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration
                .GetConnectionString("DefaultConnection");

            return services.AddDbContext<Dsw2026Ej15DbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        }
    }
}