using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace TelegramGlassBot;

public class BotHandler
{
    private readonly ITelegramBotClient _botClient;
    private readonly PriceService _priceService;

    public BotHandler(ITelegramBotClient botClient)
    {
        _botClient = botClient;
        _priceService = new PriceService();
    }

    public async Task HandleUpdateAsync(Update update)
    {
        if (update.Type != UpdateType.Message || update.Message?.Text == null) return;

        var chatId = update.Message.Chat.Id;
        var userInput = update.Message.Text;

        var matches = _priceService.SearchByModel(userInput);
        var response = _priceService.FormatResponse(matches);

        await _botClient.SendTextMessageAsync(chatId, response, parseMode: ParseMode.Markdown);
    }
}