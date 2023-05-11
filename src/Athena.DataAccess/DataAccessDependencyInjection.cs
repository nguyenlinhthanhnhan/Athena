using Athena.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Athena.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresqlConnection"));
        });
        
        return serviceCollection;
    }
}