using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace Cegeka.Guild.Pokeverse.Persistence.Mongo
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMongoStorage(this IServiceCollection services)
        {
            return services
                .AddMongoSettings()
                .AddMongoClient()
                .AddScoped<IMongoStorage, MongoStorage>();
        }

        private static IServiceCollection AddMongoSettings(this IServiceCollection services)
        {
            return services.AddSingleton(provider =>
                provider.GetService<IConfiguration>()
                    .GetSection(nameof(MongoSettings))
                    .Get<MongoSettings>(o => o.BindNonPublicProperties = true));
        }

        private static IServiceCollection AddMongoClient(this IServiceCollection services)
        {
            return services.AddScoped(provider =>
            {
                var settings = provider.GetService<MongoSettings>();
                return new MongoClient(settings.ConnectionString);
            });
        }
    }
}