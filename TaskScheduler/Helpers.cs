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

    public static void Initialize(this MenuActionService menuActionService)
    {
        menuActionService.AddNewAction(1, "Add Task", MenuTypes.MainMenu);
        menuActionService.AddNewAction(2, "Show Task", MenuTypes.MainMenu);
        menuActionService.AddNewAction(3, "Exit", MenuTypes.MainMenu);

        menuActionService.AddNewAction(1, "Show all tasks", MenuTypes.TaskMenu);
        menuActionService.AddNewAction(2, "Show tasks by given year", MenuTypes.TaskMenu);
        menuActionService.AddNewAction(3, "Show tasks between given date range", MenuTypes.TaskMenu);
        menuActionService.AddNewAction(4, "Show tasks on given day", MenuTypes.TaskMenu);
        menuActionService.AddNewAction(5, "Cancel", MenuTypes.TaskMenu);
    }
}
