using Microsoft.AspNetCore.Diagnostics;
using TankGameAPI.Services;
using TankGameAPI.Utils;

namespace TankGameAPI.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Prepares database configuring and seeding data
        /// </summary>
        /// <param name="app"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        public static async Task<IApplicationBuilder> PrepareDatabase(this IApplicationBuilder app, FieldInfo fieldInfo)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;
            var fieldService = serviceProvider.GetRequiredService<IFieldService>();

            await fieldService.CreateField(fieldInfo.Height, fieldInfo.Width, fieldInfo.Obstacles);

            return app;
        }

        /// <summary>
        /// Setups custom exception
        /// </summary>
        /// <param name="app"></param>
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
