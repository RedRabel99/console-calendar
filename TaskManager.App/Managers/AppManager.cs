using ConsoleCalendar.App.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.App.Abstract;
using TaskManager.App.Helpers;
using TaskManager.App.Managers;
using TaskManager.Domain.Helpers;

namespace TaskManager;

public class AppManager
{
    private readonly ICalendarEventService calendarEventService;
    private readonly IMenuActionService menuActionService;

    public AppManager(ICalendarEventService calendarEventService)
    {
        this.calendarEventService = calendarEventService;
        this.menuActionService = new MenuActionService();
        InitializeMainMenuActionService();
    }
    public void Run()
    {
        var taskViews = new CalendarEventManager(calendarEventService);
        Console.WriteLine("Welcome to Task Manager\n");

        while (true)
        {
            Console.WriteLine("Please enter what action do you want to take:");

            foreach (var menuAction in menuActionService.GetMenuActionsByMenuName(MenuType.MainMenu))
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
            }
            var operation = Console.ReadKey();
            Console.WriteLine();

            switch (operation.KeyChar)
            {
                case '1':
                    taskViews.AddTaskView();
                    break;
                case '2':
                    taskViews.GetTasksView();
                    break;
                case '3':
                    return;
                default:
                    Console.WriteLine("Entered action does not exist");
                    break;
            }
        }
    }

    private void InitializeMainMenuActionService()
    {
        menuActionService.AddNewAction(1, "Add Task", MenuType.MainMenu);
        menuActionService.AddNewAction(2, "Show Task", MenuType.MainMenu);
        menuActionService.AddNewAction(3, "Exit", MenuType.MainMenu);
    }
}
