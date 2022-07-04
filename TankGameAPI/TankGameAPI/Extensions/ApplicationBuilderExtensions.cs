using Mapster;
using MapsterMapper;
using TankGameAPI.Mapping;
using TankGameAPI.Services;
using TankGameInfrastructure;

namespace TankGameAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;
            var context = serviceProvider.GetRequiredService<Context>();
            var fieldService = serviceProvider.GetRequiredService<IFieldService>();

            await fieldService.CreateField(100, 100);

            return app;
        }

        public static void AddServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ITankService, TankService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IFieldService, FieldService>();
        }

        public static void AddMapper(this WebApplicationBuilder builder)
        {
            var config = new TypeAdapterConfig();
            config.Apply(new TankMappings());
            config.Apply(new UserMappings());
            builder.Services.AddSingleton(config);
            builder.Services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
