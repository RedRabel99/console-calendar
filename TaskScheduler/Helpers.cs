using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public enum MenuTypes
{
    MainMenu,
    TaskMenu
}

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

    public static void PrintMenu(MenuActionService menuActionService, MenuTypes menuName)
    {
        foreach (var menuAction in menuActionService.GetMenuActionsByMenuName(menuName))
        {
            Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
        }
    }
}

public class InvalidDateRangeException : Exception
{
    public InvalidDateRangeException() { }

    public InvalidDateRangeException(string message)
        : base(message) { }

    public InvalidDateRangeException(string message, Exception inner)
        : base(message, inner) { }
}
