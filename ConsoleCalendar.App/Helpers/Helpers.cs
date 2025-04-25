using ConsoleCalendar.App.Abstract;
using ConsoleCalendar.Domain.Helpers;
using Newtonsoft.Json;

namespace ConsoleCalendar.App.Helpers;

public static class Extensions
{
    public static bool Between(this DateTime date, DateTime start, DateTime end, bool inclusive = true)
    {
        if (inclusive)
        {
            return date >= start && date <= end;
        }

        return date >= start && date <= end;
    }

    public static void PrintMenu(IMenuActionService menuActionService, MenuType menuName)
    {
        foreach (var menuAction in menuActionService.GetMenuActionsByMenuName(menuName))
        {
            Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
        }
    }

    public static string Truncate(this string text, int maxLength)
    {
        if (string.IsNullOrEmpty(text)) return "";
        return text.Length <= maxLength ? text : text.Substring(0, maxLength - 3) + "...";
    }
}


