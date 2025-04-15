using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class Task
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public Task(string name, string description, DateTime startDate, DateTime endDate)
    {
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
