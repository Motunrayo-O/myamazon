using MyAmazon.Data.Repository;
using MyAmazon.Data.Repository.Interfaces;

namespace MyAmazon.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureRepositoryWrapper(this IServiceCollection services)
    {
        services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
    }
}