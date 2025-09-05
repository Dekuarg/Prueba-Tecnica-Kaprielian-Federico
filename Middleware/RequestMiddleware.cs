using NLog;
using Prueba_Tecnica_Kaprielian.Controllers;
using System.Globalization;

namespace Prueba_Tecnica_Kaprielian.Middleware
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestMiddleware> _logger;

        public RequestMiddleware(RequestDelegate next, ILogger<RequestMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
             await _next(context);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Se produjo un error interno en el servidor");

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";

                var errorResponse = new { message = "Se produjo un error interno en el servidor" };

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
