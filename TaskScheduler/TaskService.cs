using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class TaskService
{
    private List<Task> tasks = new List<Task>();

    public bool Add(Task task)
    {
        tasks.Add(task); return true;
    }

    public List<Task> GetAllTasks()
    {
        return tasks;
    }

    public bool Remove(Guid id)
    {
        var itemsRemoved = tasks.RemoveAll(task => task.Id == id);
        return itemsRemoved > 0;
    }

    public List<Task> GetTasksByDateRange(DateTime startTime, DateTime endtime)
    {
        return tasks.Where(t =>
            t.StartDate.Between(startTime, endtime) ||
            t.EndDate.Between(startTime, endtime)
        ).ToList();
    }

    public List<Task> GetTasksByYear(int year)
    {
        return tasks.Where(t => t.StartDate.Year == year || t.EndDate.Year == year).ToList();
    }

    public List<Task> GetTaskByDate(DateTime date)
    {
        return tasks.Where(t =>
            t.StartDate.Date ==  date.Date ||
            t.EndDate.Date == date.Date
        ).ToList();
    }

    public void AddTaskView()
    {
        Console.WriteLine("Enter task name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter task description;");
        var description =  Console.ReadLine();

        string[] formats = { "yyyy-MM-dd HH:mm:ss", "yyyy-MM-dd HH:mm", "yyyy-MM-dd" };
        Console.WriteLine("Enter start date (yyyy-MM-dd [HH:mm[:ss]]): ");

        var startDate = ParseDate(Console.ReadLine(), formats);
        if(startDate is null){
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
            Console.WriteLine("Your task has not been added");
            return;
        }
        Console.WriteLine("Enter start date (yyyy-MM-dd [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        if(endDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
            Console.WriteLine("Your task has not been added");
            return;
        }

        var task = new Task(name, description, startDate.Value, endDate.Value);
        Add(task);
    }

    private static DateTime? ParseDate(string date, string[] formats)
    {
        var wasParseSuccesfull = DateTime.TryParseExact(
            date,
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var resultDate
            );
        
        return wasParseSuccesfull ? resultDate : null;
    }

}
