using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.App.Abstract
{
    public interface ICalendarEventService
    {
        public bool Add(CalendarEvent calendarEvent);
        public bool Remove(int id);
        public bool Update(int id, CalendarEvent calendarEvent);
        List<CalendarEvent> GetAllTasks();
        int GetLastId();
        List<CalendarEvent> GetTasksByDateRange(DateTime startTime, DateTime endtime);
        List<CalendarEvent> GetTasksByYear(int year);
        List<CalendarEvent> GetTaskByDate(DateTime date);
    }
}
