using Microsoft.EntityFrameworkCore;
using TankGameAPI.Extensions;
using TankGameAPI.Utils;
using TankGameInfrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("Cors", policy =>
    {
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
        policy.AllowAnyOrigin();
        policy.WithExposedHeaders("Content-Disposition");
    });
});
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options => options.UseInMemoryDatabase("tank"));
builder.Services.Configure<FieldInfo>(builder.Configuration.GetSection("FieldInfo"));

builder.AddServices();
builder.AddMapper();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


var fieldInfo = builder.Configuration.GetSection("FieldInfo").Get<FieldInfo>();
await app.PrepareDatabase(fieldInfo);

app.SetupExceptionHandler();

app.UseHttpsRedirection();

app.UseCors("Cors");

app.UseAuthorization();

app.MapControllers();

app.Run();
