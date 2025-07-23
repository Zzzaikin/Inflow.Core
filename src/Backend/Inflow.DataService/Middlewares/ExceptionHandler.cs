using System.Net;
using System.Data.SqlClient;
using Microsoft.Extensions.Localization;
using Inflow.Data.DTO.DataRequest;

namespace Inflow.DataService.Middlewares
{
    public class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        private readonly ILogger<ExceptionHandler> _logger;

        private readonly IStringLocalizer<ExceptionHandler> _stringLocalizer;

        public ExceptionHandler(
            RequestDelegate next,
            ILogger<ExceptionHandler> logger,
            IStringLocalizer<ExceptionHandler> stringLocalizer)
        {
            _next = next;
            _logger = logger;
            _stringLocalizer = stringLocalizer;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            await HandleExceptionAsync(httpContext);
        }

        private async Task HandleExceptionAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
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
                var clientMessage = _stringLocalizer["SqlError"];
                await LogExceptionAndSendErrorResponseAsync(sqlException, clientMessage,
                    httpContext, HttpStatusCode.InternalServerError);
            }

            catch (Exception exception)
            {
                var clientMessage = _stringLocalizer["AnUnexpectedErrorHasOccurred"];
                await LogExceptionAndSendErrorResponseAsync(exception, clientMessage,
                    httpContext, HttpStatusCode.InternalServerError);
            }
        }

        private async Task LogExceptionAndSendErrorResponseAsync(Exception exception, string clientMessage, 
            HttpContext httpContext, HttpStatusCode httpStatusCode)
        {
            _logger.LogError(exception.ToString());
            await SendErrorResponseAsync(httpContext, httpStatusCode, clientMessage);
        }

        private async Task SendErrorResponseAsync(HttpContext httpContext, 
            HttpStatusCode httpStatusCode, string message)
        {
            var error = new Error
            {
                Message = message
            };

            var httpResponse = httpContext.Response;

            httpResponse.ContentType = "application/json";
            httpResponse.StatusCode = (int)httpStatusCode;

            await httpResponse.WriteAsJsonAsync(error);
        }
    }
}
