using Kedu.Api.Filters;
using Kedu.Api.Middlewares;
using Kedu.Application;
using Kedu.Infra.Data.EF;
using Kedu.Infra.Extensions;

var builder = WebApplication.CreateBuilder(args);

var appConfigs = builder.AddAppConfigs();

builder.Services
    .AddApplication()
    .AddAppConnections(appConfigs)
    .AddRepositories(appConfigs);

builder.Services.AddControllers(options =>
            options.Filters.Add(typeof(ExceptionFilter)));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ValidationMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
