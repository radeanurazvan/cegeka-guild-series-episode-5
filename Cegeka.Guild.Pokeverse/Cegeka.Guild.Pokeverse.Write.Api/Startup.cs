using Cegeka.Guild.Pokeverse.Business;
using Cegeka.Guild.Pokeverse.Infrastructure;
using Cegeka.Guild.Pokeverse.Persistence.EntityFramework;
using Cegeka.Guild.Pokeverse.Persistence.EventStoreDb;
using Cegeka.Guild.Pokeverse.RabbitMQ;
using Cegeka.Guild.Pokeverse.RabbitMQ.ContractResolvers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace Cegeka.Guild.Pokeverse.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var contractResolver = new PrivateCamelCasePropertyContractResolver();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = contractResolver
            };

            services
                .AddInfrastructure(Configuration)
                .AddCore()
                .AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}))
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app
                .UseSwagger()
                .UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"))
                .UseHttpsRedirection()
                .UseRouting()
                .UseAuthorization()
                .UseRabbitMqEventsLogging()
                .UseSubscriptions()
                .UseEventStore()
                .UseEndpoints(endpoints => endpoints.MapControllers())
                .UseMigrations()
                .SeedData();
        }
    }
}
