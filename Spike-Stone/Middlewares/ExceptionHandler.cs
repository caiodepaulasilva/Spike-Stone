using Domain.Exceptions;
using System.Net;

namespace Spike_Stone.Middlewares
{
    public class ExceptionHandler(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)GetStatusCode(exception);            

            var errorMessage = exception.InnerException?.Message ?? exception.Message;
            
            var jsonMesage = $"{{\"message\": \"{errorMessage}\"}}";

            await context.Response.WriteAsync(jsonMesage);
        }

        public HttpStatusCode GetStatusCode(Exception exception)
        {
            if (exception is not BaseException internalException)
            {
                return HttpStatusCode.InternalServerError;
            }
            return internalException.StatusCode;
        }
    }
}