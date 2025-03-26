using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;

namespace TelegramGlassBot
{
    public class UpdateHandler : IUpdateHandler
    {
        private readonly BotHandler _handler;

        public UpdateHandler(ITelegramBotClient botClient)
        {
            _handler = new BotHandler(botClient);
        }

        // ✅ Обязательная реализация интерфейса
        public Task HandleUpdateAsync(
            ITelegramBotClient botClient,
            Update update,
            CancellationToken cancellationToken)
        {
            return _handler.HandleUpdateAsync(update);
        }

        // ✅ Обязательная реализация интерфейса
        public Task HandlePollingErrorAsync(
            ITelegramBotClient botClient,
            Exception exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine($"❌ Ошибка в polling: {exception.Message}");
            return Task.CompletedTask;
        }

        public Task HandleErrorAsync(
             ITelegramBotClient botClient,
             Exception exception,
             HandleErrorSource source,
             CancellationToken cancellationToken)
        {
            Console.WriteLine($"❌ Ошибка Telegram API [{source}]: {exception.Message}");
            return Task.CompletedTask;
        }
    }
}
