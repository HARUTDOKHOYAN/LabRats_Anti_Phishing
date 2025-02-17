using System.Net;
using LearningASPweb.Exceptions;
using LearningASPweb.Exceptions.ExeptionsModels;
using Newtonsoft.Json;

namespace LearningASPweb.Middlewares;

public class ExeptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionMiddleware> _logger;

    public ExeptionMiddleware(RequestDelegate next , ILogger<ExeptionMiddleware> logger)
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
            _logger.LogError(e ,"Something is wrong");
            await HandleExeptionAsync(context, e);
        }
    }

    private  Task HandleExeptionAsync(HttpContext context, Exception ex)
    {

        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var message = new ASPWebErrorModel
        {
            ErrorType = "Internal Server Error",
            ErrorMessage = ex.Message,
        }; 
        switch (ex)
        {
            case UserNotFoundException _:
                statusCode = HttpStatusCode.NotFound;
                message.ErrorType = "User not found";
                break;
        }
        
        var resins = JsonConvert.SerializeObject(message);
        context.Response.StatusCode = (int)statusCode;
        return  context.Response.WriteAsync(resins);
   
    }
}