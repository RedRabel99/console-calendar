using ConsoleCalendar.App.Abstract;

namespace ConsoleCalendar;

internal class Program
{
    static void Main(string[] args)
    {
        ICalendarEventService calendarEventService = new CalendarEventService();
        //seed initial data
        var event1 = new CalendarEvent(1,
            "Work meeting",
            "",
            new DateTime(2025, 4, 1, 15, 0, 0),
            new DateTime(2025, 4, 1, 15, 30, 0)
        );

        var event2 = new CalendarEvent(2,
            "Holidays",
            "Holidays with parents",
            new DateTime(2025, 6, 25),
            new DateTime(2025, 7, 4)
        );

        calendarEventService.Add(event1);
        calendarEventService.Add(event2);

        var app = new AppManager(calendarEventService);
        
        app.Run();
    }
}
