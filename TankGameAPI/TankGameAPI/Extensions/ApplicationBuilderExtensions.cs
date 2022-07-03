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
    }
}
