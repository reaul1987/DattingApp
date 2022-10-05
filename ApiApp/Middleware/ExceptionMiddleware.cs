using ApiApp.Error;
using System.Net;
using System.Text.Json;

namespace ApiApp.Middleware
{
    public class ExceptionMiddleware
    {
        public RequestDelegate Next { get; }
        public ILogger<ExceptionMiddleware> Logger { get; }
        public IHostEnvironment Env { get; }

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
        {
            Next = next;
            Logger = logger;
            Env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.Next(context);
            }
            catch (Exception ex)
            {
                this.Logger.LogError(ex, ex.Message);
                context.Response.ContentType = "aaplication/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = this.Env.IsDevelopment() ? new ApiException(context.Response.StatusCode, ex.Message, ex.StackTrace?.ToString()) :
                    new ApiException(context.Response.StatusCode, "Internal Server Error");

                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);
                   
                await context.Response.WriteAsync(json);
            }
        }
    }
}
