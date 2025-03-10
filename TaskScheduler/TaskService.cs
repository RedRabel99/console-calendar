using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
}
