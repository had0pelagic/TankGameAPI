using Mapster;
using MapsterMapper;
using TankGameAPI.Mapping;
using TankGameAPI.Services;

namespace TankGameAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
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
