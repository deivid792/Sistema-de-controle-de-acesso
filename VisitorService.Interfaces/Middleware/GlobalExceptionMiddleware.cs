using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using VisitorService.Domain.Shared.Exceptions;

namespace VisitorService.Interfaces.Middleware
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _Logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _Logger = logger;
        }



        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (InfrastructureException ie)
            {
                _Logger.LogError(ie, "Erro de infraestrutura");
                await WriteResponse(context, StatusCodes.Status503ServiceUnavailable, "Serviço temporariamente indisponível.", ie.Message);
            }
            catch (unexpectedException ue)
            {
                _Logger.LogError(ue, "Erro inesperado");
                await WriteResponse(context, StatusCodes.Status503ServiceUnavailable, "Erro interno inesperado", ue.Message);
            }
            catch (DomainException de)
            {
                _Logger.LogError(de, "Erro de domínio");
                await WriteResponse(context, StatusCodes.Status400BadRequest, de.Message);
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Erro não tratado");
                await WriteResponse(context, StatusCodes.Status500InternalServerError, "Erro interno do servidor");
            }
        }

        private static async Task WriteResponse(HttpContext context, int statusCode, string message, string? detail = null)
        {
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var response = new { message = message, detail };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
    
}