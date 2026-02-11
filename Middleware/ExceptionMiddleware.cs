using System.Net;
using System.Text.Json;
using StudentEnrollmentApi.Models; 

namespace StudentEnrollmentApi.Middleware;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // This writes to your console/terminal
            _logger.LogError(ex, "An unhandled exception occurred: {Message}", ex.Message);
            
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

      
        var response = new ErrorResponse 
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error. Please try again later.",

            Details = _env.IsDevelopment() ? ex.ToString() : "Contact support if this persists."
        };

        var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        var json = JsonSerializer.Serialize(response, options);

        await context.Response.WriteAsync(json);
    }
}