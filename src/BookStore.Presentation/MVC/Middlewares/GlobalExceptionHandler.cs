namespace MVC.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate requestDelegate)
        {
            _next = requestDelegate;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex, httpContext);
            }
        }

        private async Task HandleErrorAsync(Exception exception, HttpContext httpContext)
        {
            httpContext.Response.Redirect($"/Home/Error?errorMessage={exception.Message}");
        }
    }
}
