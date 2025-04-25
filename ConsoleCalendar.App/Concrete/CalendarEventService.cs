using ConsoleCalendar.App.Abstract;
using ConsoleCalendar.App.Helpers;
using Newtonsoft.Json;

namespace ConsoleCalendar;

public class CalendarEventService : ICalendarEventService
{
    private List<CalendarEvent> calendarEvents;
    private readonly IStorageService<CalendarEvent> storageService;
    public CalendarEventService(IStorageService<CalendarEvent> storageService)
    {
        this.storageService = storageService;
        calendarEvents = HandleLoad();
    }

    public CalendarEvent? Get(int id)
    {
        return calendarEvents.FirstOrDefault(e => e.Id == id);
    }

    public bool Add(CalendarEvent calendarEvent)
    {
        calendarEvents.Add(calendarEvent);
        HandleSave();
        return true;
    }

    public List<CalendarEvent> GetAllCalendarEvents()
    {
        return calendarEvents;
    }

    public bool Remove(int id)
    {
        var itemsRemoved = calendarEvents.RemoveAll(ce => ce.Id == id);
        HandleSave();
        return itemsRemoved > 0;
    }

    public bool Update(int id, CalendarEvent calendarEvent)
    {
        var index = calendarEvents.FindIndex(t => t.Id == id);
        if (index == -1) return false;
        calendarEvents[index] = calendarEvent;

        HandleSave();
        return true;
    }

    public List<CalendarEvent> GetCalendarEventsByDateRange(DateTime startTime, DateTime endtime)
    {
        return calendarEvents.Where(t =>
            t.StartDate.Between(startTime, endtime) ||
            t.EndDate.Between(startTime, endtime)
        ).ToList();
    }

    public List<CalendarEvent> GetCalendarEventsByYear(int year)
    {
        return calendarEvents.Where(t => t.StartDate.Year <= year && t.EndDate.Year >= year).ToList();
    }

    public List<CalendarEvent> GetCalendarEventsByDate(DateTime date)
    {
        return calendarEvents.Where(t =>
            t.StartDate.Date ==  date.Date ||
            t.EndDate.Date == date.Date
        ).ToList();
    }

    public int GetLastId()
    {
        return calendarEvents.Count == 0 ? 0 : calendarEvents.Max(t => t.Id);
    }

    private void HandleSave()
    {
        try
        {
            storageService.Save(calendarEvents);
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Could not save data: No permission to write the file: " + ex.Message);
        }
        catch (IOException ex)
        {
            Console.WriteLine("Could not save data: IO error during save: " + ex.Message);
        }
        catch (JsonSerializationException ex)
        {
            Console.WriteLine("Could not save data: Error serializing data: " + ex.Message);
        }
    }

    private List<CalendarEvent> HandleLoad()
    {
        try
        {
            return storageService.Load();
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Could not read saved data: Invalid JSON format: " + ex.Message);
            return new List<CalendarEvent>();
        }
        catch (UnauthorizedAccessException ex)
        {
            Console.WriteLine("Could not read saved data: No permission to read the file: " + ex.Message);
            return new List<CalendarEvent>();
        }
        catch (IOException ex)
        {
            Console.WriteLine("Could not read saved data: IO error: " + ex.Message);
            return new List<CalendarEvent>();
        }
    }
}
