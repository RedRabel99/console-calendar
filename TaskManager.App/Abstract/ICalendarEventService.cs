using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleCalendar.App.Abstract
{
    public interface ICalendarEventService
    {
        public bool Add(CalendarEvent calendarEvent);
        public bool Remove(int id);
        public bool Update(int id, CalendarEvent calendarEvent);
        List<CalendarEvent> GetAllCalendarEvents();
        int GetLastId();
        List<CalendarEvent> GetCalendarEventsByDateRange(DateTime startTime, DateTime endtime);
        List<CalendarEvent> GetCalendarEventsByYear(int year);
        List<CalendarEvent> GetCalendarEventsByDate(DateTime date);
    }
}
