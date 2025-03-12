using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class Task(string name, string Description, DateTime startDate, DateTime endDate)
{
    public Guid Id { get; set; } = new Guid();
    public string Name { get; set; } = name;
    public string Description { get; set; } = Description;
    public DateTime StartDate { get; set; } = startDate;
    public DateTime EndDate { get; set; } = endDate;
}
