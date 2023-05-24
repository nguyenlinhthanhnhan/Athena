using Athena.DataAccess.Persistence;
using Athena.DataAccess.Repositories;
using Athena.DataAccess.Repositories.Impl;
using Athena.Shared.Common;
using Athena.Shared.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Athena.DataAccess;

public static class DataAccessDependencyInjection
{
    public static IServiceCollection AddInfrastructureData(this IServiceCollection serviceCollection,
        IConfiguration configuration)
    {
        serviceCollection.AddDbContext<AthenaDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgresqlConnection"));
        });

        serviceCollection.AddTransient<IDateTime, DateTimeService>();
        serviceCollection.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));

        return serviceCollection;
    }
}