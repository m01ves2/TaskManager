namespace TaskManager.Middleware
{
    public class ExceptionCatcher
    {
        private readonly RequestDelegate _next;
        public ExceptionCatcher(RequestDelegate next)
        {
            _next = next;
        }

        //async public Task ProcessRequest(HttpContext context, RequestDelegate next)
        //{
        //    try {
        //        await next(context);
        //    }
        //    catch (Exception e) {
        //        context.Response.StatusCode = 500;
        //        await context.Response.WriteAsJsonAsync(new { message = $"Internal server error: {e.Message}" });
        //    }
        //}

        public async Task Invoke(HttpContext context)
        {
            try {
                await _next(context);
            }
            catch (Exception e) {
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new
                {
                    message = $"Internal server error: {e.Message}"
                });
            }
        }
    }
}
