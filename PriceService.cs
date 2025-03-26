using System.Data.SQLite;
using System.Text;
using TelegramGlassBot.Models;

namespace TelegramGlassBot;

public class PriceService
{
    private readonly string _dbPath = "glass_prices.db";

    public List<PriceOpt> SearchByModel(string userInput)
    {
        var results = new List<PriceOpt>();

        using var connection = new SQLiteConnection($"Data Source={_dbPath}");
        connection.Open();

        using var cmd = connection.CreateCommand();
        cmd.CommandText = @"
            SELECT Mark, Model, Problem, Price 
            FROM PriceOpts 
            WHERE LOWER (Mark || ' ' || Model) LIKE @input";
        cmd.Parameters.AddWithValue("@input", $"%{userInput.ToLower()}%");

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            results.Add(new PriceOpt
            {
                Mark = reader.GetString(0),
                Model = reader.GetString(1),
                Problem = reader.GetString(2),
                Price = reader.GetString(3)
            });
        }

        return results;
    }

    public string FormatResponse(List<PriceOpt> items)
    {
        if (items == null || items.Count == 0)
            return "Такой модели нет 😔 Попробуйте уточнить модель или свяжитесь с нами";

        var grouped = items
            .GroupBy(i => $"{i.Mark} {i.Model}")
            .ToList();

        var sb = new StringBuilder();

        foreach (var group in grouped)
        {
            sb.AppendLine($"📱 *{group.Key}*");

            foreach (var item in group)
            {
                sb.AppendLine($"— {item.Problem}: {item.Price}");
            }

            sb.AppendLine();
        }

        return sb.ToString();
    }
}