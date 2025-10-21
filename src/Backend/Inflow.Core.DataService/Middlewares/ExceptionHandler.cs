using System.Net;
using Inflow.Core.Data.DTO.DataRequest;
using Microsoft.Extensions.Localization;
using Microsoft.Data.SqlClient;

namespace Inflow.Core.DataService.Middlewares;

public class ExceptionHandler(
    RequestDelegate next,
    ILogger<ExceptionHandler> logger,
    IStringLocalizer<ExceptionHandler> stringLocalizer)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        await HandleExceptionAsync(httpContext);
    }

    private async Task HandleExceptionAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (ArgumentException argumentException)
        {
            await LogExceptionAndSendErrorResponseAsync(argumentException, argumentException.Message,
                httpContext, HttpStatusCode.BadRequest);
        }
        catch (NotImplementedException notImplementedException)
        {
            await LogExceptionAndSendErrorResponseAsync(notImplementedException, 
                notImplementedException.Message, httpContext, HttpStatusCode.BadRequest);
        }
        catch (SqlException sqlException)
        {
            var clientMessage = stringLocalizer["SqlError"];
            await LogExceptionAndSendErrorResponseAsync(sqlException, clientMessage,
                httpContext, HttpStatusCode.InternalServerError);
        }
        catch (Exception exception)
        {
            var clientMessage = stringLocalizer["AnUnexpectedErrorHasOccurred"];
            await LogExceptionAndSendErrorResponseAsync(exception, clientMessage,
                httpContext, HttpStatusCode.InternalServerError);
        }
    }

    private async Task LogExceptionAndSendErrorResponseAsync(Exception exception, string clientMessage, 
        HttpContext httpContext, HttpStatusCode httpStatusCode)
    {
        logger.LogError(exception.ToString());
        await SendErrorResponseAsync(httpContext, httpStatusCode, clientMessage);
    }

    private async Task SendErrorResponseAsync(HttpContext httpContext, 
        HttpStatusCode httpStatusCode, string message)
    {
        var error = new Error { Message = message };
        var httpResponse = httpContext.Response;
        httpResponse.ContentType = "application/json";
        httpResponse.StatusCode = (int)httpStatusCode;
        await httpResponse.WriteAsJsonAsync(error);
    }
}