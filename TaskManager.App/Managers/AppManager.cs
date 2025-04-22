using ConsoleCalendar.App.Abstract;
using ConsoleCalendar.App.Managers;
using ConsoleCalendar.Domain.Helpers;

namespace ConsoleCalendar;

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
        var calendarEventManager = new CalendarEventManager(calendarEventService);
        Console.WriteLine("Welcome to Console Calendar app\n");

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
                    calendarEventManager.AddCalendarEventView();
                    break;
                case '2':
                    calendarEventManager.GetCalendarEventsView();
                    break;
                case '3':
                    calendarEventManager.EditCalendarEventView();
                    break;
                case '4':
                    calendarEventManager.RemoveCalendarEventView();
                    break;
                case '5':
                    return;
                default:
                    Console.WriteLine("Entered action does not exist");
                    break;
            }
        }
    }

    private void InitializeMainMenuActionService()
    {
        menuActionService.AddNewAction(1, "Add calendar event", MenuType.MainMenu);
        menuActionService.AddNewAction(2, "Show calendar event", MenuType.MainMenu);
        menuActionService.AddNewAction(3, "Edit calendar event", MenuType.MainMenu);
        menuActionService.AddNewAction(4, "Remove calendar event", MenuType.MainMenu);
        menuActionService.AddNewAction(5, "Exit", MenuType.MainMenu);
    }
}
