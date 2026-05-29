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
                context.Response.StatusCode = 400;
                await context.Response.WriteAsJsonAsync(new
                {
                    message = e.Message,
                    code = e.Code,
                });
            }
            catch (Exception e) {
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    code = "INTERNAL_SERVER_ERROR",
                    message = $"Internal server error: {e.Message}"
                });
            }
        }
    }
}
