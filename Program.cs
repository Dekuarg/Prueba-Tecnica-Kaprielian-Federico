using Microsoft.OpenApi.Models;
using Polly;
using Microsoft.Extensions.Http.Resilience;
using Polly.Fallback;
using Prueba_Tecnica_Kaprielian.Interfaces;
using Prueba_Tecnica_Kaprielian.Services;
using System.Net;
using Prueba_Tecnica_Kaprielian.Modelos;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using NLog;
using NLog.Web;
using System;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Dominio.Interfaces;
using Dominio.Modelos;
using Infraestructura.Repositories;
using Microsoft.EntityFrameworkCore;
using Infraestructura.CarpetaPrincipal;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Logica.Services;


var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();


try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    builder.Services.AddControllers();

    builder.Logging.ClearProviders();
    builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
    builder.Host.UseNLog();

    builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("con") ?? throw new Exception("sin connectionstring"), ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("con"))));

    builder.Services.AddScoped<IRepository, ObjectosApiRepository>();
    builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

    builder.Services.AddHealthChecks().AddCheck<HealthCheckServices>("HealthCheck", HealthStatus.Unhealthy);

    builder.Services.AddHttpClient<IApiObject, ApiObject>(client =>
    {
        client.BaseAddress = new Uri("https://api.restful-api.dev");

    }).AddResilienceHandler("WithFallBack", resilienceBuilder =>
    {
        resilienceBuilder.AddFallback(new FallbackStrategyOptions<HttpResponseMessage>
        {

            ShouldHandle = new PredicateBuilder<HttpResponseMessage>()
                               .Handle<HttpRequestException>()
                               .HandleResult(r => !r.IsSuccessStatusCode),
            FallbackAction = async args =>
            {
                ErrorResponse errorResponse = new()
                {
                    Status = (int)args.Outcome.Result.StatusCode,
                    Title = $"Error en la request: {args.Outcome.Result.RequestMessage?.RequestUri}",
                    Description = await args.Outcome.Result.Content.ReadAsStringAsync(),
                    TraceId = Guid.NewGuid().ToString()
                };
                var fallbackResponse = new HttpResponseMessage(args.Outcome.Result.StatusCode)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(errorResponse))
                };
                return Outcome.FromResult(fallbackResponse);
            },
            OnFallback = async args =>
            {
                logger.Error($" Error: {await args.Outcome.Result.Content.ReadAsStringAsync()} | Request: {args.Outcome.Result.RequestMessage?.RequestUri}");
                return;
            }
        });
    });

    builder.Services.AddHttpClient<IRandomUserApi,ApiRandomUser>(client =>
    {
        client.DefaultRequestHeaders.Add("Surtechnology", "6E3F37EF-2DBC-4062-B974-5812DCB0B2AC");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    app.MapHealthChecks("/Health/Index");
    // Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", ""));

    app.UseMiddleware<Prueba_Tecnica_Kaprielian.Middleware.RequestMiddleware>();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception e)
{
    logger.Error(e, "Stopped program because of exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}

