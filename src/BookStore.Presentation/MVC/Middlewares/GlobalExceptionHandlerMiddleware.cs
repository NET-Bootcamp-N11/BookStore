
using BookStore.Domain.Exceptions;

namespace MVC.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public GlobalExceptionHandlerMiddleware(RequestDelegate requestDelegate)
            => _requestDelegate = requestDelegate;

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _requestDelegate(context);
            }
            catch (NotFoundException ex)
            {
                int code = 404;
                await HandleExceptionAsync(context, ex, code);
            }
            catch (ValidationException ex)
            {
                int code = 400;
                await HandleExceptionAsync(context, ex, code);
            }
            catch (Exception ex)
            {
                int code = 500;
                await HandleExceptionAsync(context, ex, code);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, int code)
        {
            context.Response.Redirect($"/Exceptions/Error?message={ex.Message}&code={code}");
            return;
        }
    }
}
