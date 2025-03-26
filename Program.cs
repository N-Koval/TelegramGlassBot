using Telegram.Bot;
using Telegram.Bot.Polling;

namespace TelegramGlassBot
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var botClient = new TelegramBotClient("8044959358:AAEH6KvfiCmUzH-O5Vv3Plxc3d5_Lhl3yiE");

            using var cts = new CancellationTokenSource();

            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { } // Получать все типы обновлений
            };

            var updateHandler = new UpdateHandler(botClient);

            botClient.StartReceiving(
                updateHandler: updateHandler,
                receiverOptions: receiverOptions,
                cancellationToken: cts.Token
            );

            var me = await botClient.GetMeAsync();
            Console.WriteLine($"Бот @{me.Username} запущен. Нажмите Enter для выхода.");
            await Task.Delay(Timeout.Infinite);

            cts.Cancel();
        }
    }
}