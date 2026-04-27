using Telegram.Bot;

namespace BookStore.Application.useCases.Extensions.TelegramBot
{

    public class WriteToTelegramBotService : IWriteToTelegramBotService
    {
        private readonly long _groupId = 5569322769;
        private readonly TelegramBotClient _botClient;

        public WriteToTelegramBotService(TelegramBotClient botClient)
        {
            _botClient = botClient;
        }

        public async Task LogError(Exception ex, CancellationToken cancellationToken)
        {
            string errorMessage = $"An error occurred:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}";
            await _botClient.SendTextMessageAsync(chatId: _groupId, text: ex.ToString(), cancellationToken: cancellationToken);
        }

    }
}
