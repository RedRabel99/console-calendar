using ConsoleCalendar.App.Abstract;
using ConsoleCalendar.App.Concrete;

namespace ConsoleCalendar;

internal class Program
{
    static void Main(string[] args)
    {
        var filePath = Environment.ExpandEnvironmentVariables(
            @"%USERPROFILE%\Documents\ConsoleCalendar\events.json");


        IStorageService<CalendarEvent> storageService = new CalendarEventJsonStorageService(filePath);
        ICalendarEventService calendarEventService = new CalendarEventService(storageService);
        var app = new AppManager(calendarEventService);
        
        app.Run();
    }
}
