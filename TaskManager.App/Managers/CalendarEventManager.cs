using ConsoleCalendar.App.Abstract;
using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using ConsoleCalendar.App.Abstract;
using ConsoleCalendar.App.Helpers;
using ConsoleCalendar.Domain.Helpers;

namespace ConsoleCalendar.App.Managers;

public class CalendarEventManager
{
    private ICalendarEventService calendarEventService;
    private IMenuActionService menuActionService;
    private readonly string[] formats = {
        "dd-MM-yyyy HH:mm:ss",
        "dd-MM-yyyy HH:mm",
        "dd-MM-yyyy"
    };

    public CalendarEventManager(ICalendarEventService calendarEventService)
    {
        this.calendarEventService = calendarEventService;
        menuActionService = new MenuActionService();
        InitializeTaskViewsActionService();
    }

    public void AddCalendarEventView()
    {
        Console.WriteLine("Enter calendar event name:");
        var name = Console.ReadLine();

        Console.WriteLine("\nEnter calendar event description;");
        var description = Console.ReadLine();


        Console.WriteLine("\nEnter start date (dd-MM-yyyy [HH:mm[:ss]]): ");

        var startDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (startDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            Console.WriteLine("Your calendar event has not been added");
            return;
        }

        Console.WriteLine("Enter start date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (endDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            Console.WriteLine("Your calendar event has not been added");
            return;
        }
        var id = calendarEventService.GetLastId() + 1;
        var calendarEvent = new CalendarEvent(id, name, description, startDate.Value, endDate.Value);
        calendarEventService.Add(calendarEvent);
    }

    public void GetCalendarEventsView()
    {
        Console.WriteLine("What calendar events do you want to view");
        Extensions.PrintMenu(menuActionService, MenuType.TaskMenu);

        var key = Console.ReadKey().KeyChar;
        Console.WriteLine();

        switch (key)
        {
            case '1':
                ShowAllCalendarEventsView();
                break;
            case '2':
                ShowCalendarEventsByYearView();
                break;
            case '3':
                ShowCalendarEventsByRangeView();
                break;
            case '4':
                ShowCalendarEventsByDateView();
                break;
            case '5':
                return;
            default:
                Console.WriteLine("Selected invalid option, try again");
                break;
        }

    }
    private void ShowAllCalendarEventsView()
    {
        var calendarEvents = calendarEventService.GetAllCalendarEvents();
        PrintCalendarEvents(calendarEvents);
    }

    private void ShowCalendarEventsByYearView()
    {
        Console.WriteLine("Enter a year:");
        if (!int.TryParse(Console.ReadLine(), out var year))
        {
            Console.WriteLine("\nGiven year is invalid, try again");
            return;
        }
        Console.WriteLine();
        var calendarEvent = calendarEventService.GetCalendarEventsByYear(year);
        PrintCalendarEvents(calendarEvent);
    }

    private void ShowCalendarEventsByRangeView()
    {
        Console.WriteLine("Enter start date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var startDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (startDate == null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            return;
        }

        Console.WriteLine("Enter end date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (endDate == null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
        }

        var calendarEvent = calendarEventService.GetCalendarEventsByDateRange(startDate.Value, endDate.Value);
        PrintCalendarEvents(calendarEvent);
    }

    private void ShowCalendarEventsByDateView()
    {
        Console.WriteLine("Enter start date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var date = ParseDate(Console.ReadLine(), formats);
        if (date == null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            return;
        }
        var calendarEvent = calendarEventService.GetCalendarEventsByDate(date.Value);
        PrintCalendarEvents(calendarEvent);
    }

    private static void PrintCalendarEvents(List<CalendarEvent> calendarEvents)
    {
        var table = new ConsoleTable("Id","Name", "Description", "Start Date", "EndDate", "Duration");
        foreach (var calendarEvent in calendarEvents)
        {
            var duration = calendarEvent.EndDate - calendarEvent.StartDate;
            table.AddRow(
                calendarEvent.Id,
                calendarEvent.Name,
                calendarEvent.Description,
                calendarEvent.StartDate.ToString(),
                calendarEvent.EndDate.ToString(),
                $"{(int)duration.TotalHours}h {duration.Minutes}m"
                );
        }
        table.Write();
    }
    private static DateTime? ParseDate(string date, string[] formats)
    {

        var wasParseSuccesfull = DateTime.TryParseExact(
            date.Trim(), //Trim is used to ensure no leading or trailing spaces could prevent parsing
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var resultDate
            );

        return wasParseSuccesfull ? resultDate : null;
    }

    private void InitializeTaskViewsActionService()
    {
        menuActionService.AddNewAction(1, "Show all calendar events", MenuType.TaskMenu);
        menuActionService.AddNewAction(2, "Show calendar events by given year", MenuType.TaskMenu);
        menuActionService.AddNewAction(3, "Show calendar events between given date range", MenuType.TaskMenu);
        menuActionService.AddNewAction(4, "Show calendar events on given day", MenuType.TaskMenu);
        menuActionService.AddNewAction(5, "Cancel", MenuType.TaskMenu);
    }
}
