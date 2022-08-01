using Microsoft.AspNetCore.Diagnostics;
using TankGameAPI.Services;
using TankGameAPI.Utils;
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

            await fieldService.CreateField(5, 5, 5);

            return app;
        }

        public static void SetupExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(exception =>
            {
                exception.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    context.Response.ContentType = "application/json";

                    if (contextFeature == null)
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        await context.Response.WriteAsync(new Error
                        {
                            Code = StatusCodes.Status500InternalServerError,
                            Message = "Server error"
                        }.ToString());
                        return;
                    }

                    switch (contextFeature.Error)
                    {
                        case InvalidClientException:
                            context.Response.StatusCode = StatusCodes.Status400BadRequest;
                            await context.Response.WriteAsync(new Error
                            {
                                Code = StatusCodes.Status400BadRequest,
                                Message = contextFeature.Error.Message
                            }.ToString());
                            break;
                        default:
                            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                            await context.Response.WriteAsync(new Error
                            {
                                Code = StatusCodes.Status500InternalServerError,
                                Message = "Server error"
                            }.ToString());
                            break;
                    }
                });
            });
        }
    }
}
