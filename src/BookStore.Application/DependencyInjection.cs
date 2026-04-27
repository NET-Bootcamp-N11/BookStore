using BookStore.Application.useCases.Extensions.TelegramBot;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Telegram.Bot;

namespace BookStore.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddSingleton<TelegramBotClient>(provider =>
            {
                var botToken = configuration.GetSection("TelegramBotToken:Token").Value;
                return new TelegramBotClient(botToken);
            });

            services.AddSingleton<IWriteToTelegramBotService, WriteToTelegramBotService>();

            return services;
        }
    }
}
