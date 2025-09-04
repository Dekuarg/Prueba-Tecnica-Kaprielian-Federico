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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

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
             Status =  (int)args.Outcome.Result.StatusCode,
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
        OnFallback = args =>
        {
            Console.WriteLine("Fallback ejecutado para request: " + args.Context.OperationKey);
            return default;
        }
    });
});

builder.Services.AddHttpClient("ApiRandom", client =>
{
    client.BaseAddress = new Uri("https://api.restful-api.dev");

});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json",""));

app.UseAuthorization();

app.MapControllers();

app.Run();
