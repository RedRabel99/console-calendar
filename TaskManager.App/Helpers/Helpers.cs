using ConsoleCalendar.App.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Helpers;

namespace TaskManager.App.Helpers;

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
}


