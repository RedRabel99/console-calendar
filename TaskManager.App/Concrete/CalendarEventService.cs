using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.App.Helpers;

namespace TaskManager;

public class CalendarEventService
{
    private List<CalendarEvent> tasks = new List<CalendarEvent>();

    public bool Add(CalendarEvent task)
    {
        tasks.Add(task); return true;
    }

    public List<CalendarEvent> GetAllTasks()
    {
        return tasks;
    }

    public bool Remove(int id)
    {
        var itemsRemoved = tasks.RemoveAll(task => task.Id == id);
        return itemsRemoved > 0;
    }

    public List<CalendarEvent> GetTasksByDateRange(DateTime startTime, DateTime endtime)
    {
        return tasks.Where(t =>
            t.StartDate.Between(startTime, endtime) ||
            t.EndDate.Between(startTime, endtime)
        ).ToList();
    }

    public List<CalendarEvent> GetTasksByYear(int year)
    {
        return tasks.Where(t => t.StartDate.Year <= year && t.EndDate.Year >= year).ToList();
    }

    public List<CalendarEvent> GetTaskByDate(DateTime date)
    {
        return tasks.Where(t =>
            t.StartDate.Date ==  date.Date ||
            t.EndDate.Date == date.Date
        ).ToList();
    }

    public int GetLastId()
    {
        return tasks.Count == 0 ? 0 : tasks.Max(t => t.Id);
    }
}
