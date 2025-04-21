using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.App.Abstract;
using TaskManager.App.Helpers;

namespace TaskManager;

public class CalendarEventService : ICalendarEventService
{
    private List<CalendarEvent> calendarEvents = new List<CalendarEvent>();

    public bool Add(CalendarEvent calendarEvent)
    {
        calendarEvents.Add(calendarEvent); return true;
    }

    public List<CalendarEvent> GetAllTasks()
    {
        return calendarEvents;
    }

    public bool Remove(int id)
    {
        var itemsRemoved = calendarEvents.RemoveAll(ce => ce.Id == id);
        return itemsRemoved > 0;
    }

    public bool Update(int id, CalendarEvent calendarEvent)
    {
        var index = calendarEvents.FindIndex(t => t.Id == id);
        if (index == -1) return false;
        calendarEvents[index] = calendarEvent;
        return true;
    }

    public List<CalendarEvent> GetTasksByDateRange(DateTime startTime, DateTime endtime)
    {
        return calendarEvents.Where(t =>
            t.StartDate.Between(startTime, endtime) ||
            t.EndDate.Between(startTime, endtime)
        ).ToList();
    }

    public List<CalendarEvent> GetTasksByYear(int year)
    {
        return calendarEvents.Where(t => t.StartDate.Year <= year && t.EndDate.Year >= year).ToList();
    }

    public List<CalendarEvent> GetTaskByDate(DateTime date)
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
}
