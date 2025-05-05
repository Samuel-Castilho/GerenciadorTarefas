using System.Net.Mime;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorTarefas.Presentation.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(ILogger<ExceptionHandlerMiddleware> logger, RequestDelegate next)
        {
            _logger = logger;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                await HandleCustomExceptionResponseAsync(context, ex);
            }
        }

        private async Task HandleCustomExceptionResponseAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = MediaTypeNames.Application.Json;
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var problem =
#if DEBUG
            new ProblemDetails()
            {
                Status = 500,
                Detail = ex.StackTrace,
                Title = ex.Message
            };
#else
            new ProblemDetails()
            {
                Status = 500,
                Detail = "Erro inesperado aconteceu, tente novamente mais tarde",
                Title = "Erro inesperado aconteceu"
            };
#endif
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var json = JsonSerializer.Serialize(problem, options);
            await context.Response.WriteAsync(json);
        }
    }

}

