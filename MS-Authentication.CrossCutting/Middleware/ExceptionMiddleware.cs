using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace MS_Authentication.CrossCutting.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro no middleware de exceções ao processar a requisição: {Path}", context.Request.Path);
            await HandleExceptionAsync(context, ex);
        }    
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var statusCode = ex switch
        {
            NullReferenceException => StatusCodes.Status400BadRequest,       // 400 - Erro de referência
            KeyNotFoundException => StatusCodes.Status404NotFound,           // 404 - Não encontrado
            ArgumentException => StatusCodes.Status400BadRequest,            // 400 - Argumento inválido
            InvalidOperationException => StatusCodes.Status400BadRequest,    // 400 - Operação inválida
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,// 401 - Não autorizado
            SqlException => StatusCodes.Status500InternalServerError,        // 500 - Erro no banco de dados
            DbUpdateException => StatusCodes.Status500InternalServerError,   // 500 - Erro ao atualizar o banco
            TimeoutException => StatusCodes.Status408RequestTimeout,         // 408 - Tempo de requisição expirado
            _ => StatusCodes.Status500InternalServerError                    // 500 - Erro inesperado
        };


        var response = new
        {
            status = statusCode,
            message = ex.Message,
            innerMessage = ex.InnerException?.Message,
            errorType = ex.GetType().Name
        };

        context.Response.StatusCode = statusCode;

        var jsonResponse = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}