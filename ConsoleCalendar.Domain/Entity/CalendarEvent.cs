using ConsoleCalendar.Domain.Common;
using ConsoleCalendar.Domain.Helpers;

namespace ConsoleCalendar;

public class CalendarEvent : BaseEnitity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public CalendarEvent(int id, string name, string description, DateTime startDate, DateTime endDate)
    {
        Id = id;
        Name = name;
        Description = description;
        if(startDate > endDate)
        {
            throw new InvalidDateRangeException("End date cannot be before start date.");
        }
        StartDate = startDate;
        EndDate = endDate;
    }
}
