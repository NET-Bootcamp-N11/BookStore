using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Application.useCases.Extensions.TelegramBot
{
    public interface IWriteToTelegramBotService
    {
        public Task LogError(Exception ex, CancellationToken cancellationToken = default);
    }
}
