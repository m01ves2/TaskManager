using TaskManager.Errors;
using TaskManager.Exceptions;

namespace TaskManager.Middleware
{
    public class ExceptionCatcher
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionCatcher> _logger;
        public ExceptionCatcher(RequestDelegate next, ILogger<ExceptionCatcher> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (BusinessException e) {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    new ErrorResponse()
                    {
                        Code = e.Code,
                        Message = e.Message,
                    }
                );
            }
            catch (Exception e) {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsJsonAsync(
                    new ErrorResponse()
                    {
                        Code = ErrorCodes.InternalServerError,
                        Message = $"Internal server error: {e.Message}"
                    }
                );
            }
        }
    }
}
