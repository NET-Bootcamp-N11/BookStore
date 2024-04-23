
using BookStore.Application.useCases.Extensions.TelegramBot;
using BookStore.Domain.Exceptions;
using Microsoft.VisualBasic;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MVC.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IWriteToTelegramBotService _writeToTelegramBotService;

        public GlobalExceptionHandlerMiddleware(RequestDelegate requestDelegate, ILogger<GlobalExceptionHandlerMiddleware> logger, TelegramBotClient bot, IWriteToTelegramBotService writeToTelegramBotService)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _writeToTelegramBotService = writeToTelegramBotService;
        }

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
                //Write to Telegram channel
                await _writeToTelegramBotService.LogError(ex);
                //Write to Log file
                _logger.LogError($"{ex}\n\n\n");

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
