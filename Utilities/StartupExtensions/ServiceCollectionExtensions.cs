using ProductCatalogManager.Utilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ProductCatalogManager.Utilities.StartupExtensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.DatabaseName = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseNameValue).Value;
                options.CollectionName = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.CollectionNameValue).Value;
            });
        }

    }
}