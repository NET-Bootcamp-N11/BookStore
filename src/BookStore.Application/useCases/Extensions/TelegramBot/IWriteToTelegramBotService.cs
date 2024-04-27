namespace BookStore.Application.useCases.Extensions.TelegramBot
{
    public interface IWriteToTelegramBotService
    {
        public Task LogError(Exception ex, CancellationToken cancellationToken = default);
    }
}
