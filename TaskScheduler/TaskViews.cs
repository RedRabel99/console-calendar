using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class TaskViews(TaskService taskService, MenuActionService menuActionService)
{
    private TaskService taskService = taskService;
    private MenuActionService menuActionService = menuActionService;
    private readonly string[] formats = {
        "yyyy-MM-dd HH:mm:ss",
        "yyyy-MM-dd HH:mm",
        "yyyy-MM-dd"
    };

    public void AddTaskView()
    {
        Console.WriteLine("Enter task name:");
        var name = Console.ReadLine();

        Console.WriteLine("Enter task description;");
        var description = Console.ReadLine();

        
        Console.WriteLine("Enter start date (yyyy-MM-dd [HH:mm[:ss]]): ");

        var startDate = ParseDate(Console.ReadLine(), formats);
        if (startDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
            Console.WriteLine("Your task has not been added");
            return;
        }
        Console.WriteLine("Enter start date (yyyy-MM-dd [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        if (endDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
            Console.WriteLine("Your task has not been added");
            return;
        }

        var task = new Task(name, description, startDate.Value, endDate.Value);
        taskService.Add(task);
    }

    public void GetTasksView()
    {
        Console.WriteLine("What tasks do you want to view");
        Extensions.PrintMenu(menuActionService, MenuTypes.TaskMenu);

        var key = Console.ReadKey().KeyChar;

        switch (key)
        {
            case '1':
                ShowAllTasksView();
                break;
            case '2':
                ShowTasksByYearView();
                break;
            case '3':
                ShowTasksByRangeView();
                break;
            case '4':
                ShowTasksByDateView();
                break;
            case '5':
                return;
            default:
                Console.WriteLine("Selected invalid option, try again");
                break;
        }

    }
    private void ShowAllTasksView()
    {
        var tasks = taskService.GetAllTasks();
        PrintTasks(tasks);
    }
    
    private void ShowTasksByYearView()
    {
        Console.WriteLine("Enter a year:");
        if(!int.TryParse(Console.ReadLine(), out var year))
        {
            Console.WriteLine("Given year is invalid, try again");
            return;
        }
        var tasks = taskService.GetTasksByYear(year);
        PrintTasks(tasks);
    }
    
    private void ShowTasksByRangeView()
    {
        Console.WriteLine("Enter start date (yyyy-MM-dd [HH:mm[:ss]]): ");
        var startDate = ParseDate(Console.ReadLine(), formats);
        if (startDate == null)
        {
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
            return;
        }

        Console.WriteLine("Enter end date (yyyy-MM-dd [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        if(endDate == null)
        {
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
        }

        var tasks = taskService.GetTasksByDateRange(startDate.Value, endDate.Value);
        PrintTasks(tasks);
    }
    
    private void ShowTasksByDateView()
    {
        Console.WriteLine("Enter start date (yyyy-MM-dd [HH:mm[:ss]]): ");
        var date = ParseDate(Console.ReadLine(), formats);
        if (date == null)
        {
            Console.WriteLine("Could not parse given date to correct format (yyyy-MM-dd [HH:mm[:ss]])");
            return;
        }
        var tasks = taskService.GetTaskByDate(date.Value);
        PrintTasks(tasks);
    }

    private static void PrintTasks(List<Task> tasks)
    {
        foreach (var task in tasks)
        {
            Console.WriteLine($"{task.Name} {task.Description}" +
                $" - Start: {task.StartDate} End: {task.EndDate}" +
                $" Duration: {task.EndDate - task.StartDate}");
        }
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
