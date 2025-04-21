using ConsoleTables;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.App.Abstract;
using TaskManager.App.Helpers;
using TaskManager.Domain.Helpers;

namespace TaskManager.App.Managers;

public class CalendarEventManager
{
    private CalendarEventService taskService;
    private MenuActionService menuActionService;
    private readonly string[] formats = {
        "dd-MM-yyyy HH:mm:ss",
        "dd-MM-yyyy HH:mm",
        "dd-MM-yyyy"
    };

    public CalendarEventManager(CalendarEventService taskService)
    {
        this.taskService = taskService;
        menuActionService = new MenuActionService();
        InitializeTaskViewsActionService();
    }

    public void AddTaskView()
    {
        Console.WriteLine("Enter task name:");
        var name = Console.ReadLine();

        Console.WriteLine("\nEnter task description;");
        var description = Console.ReadLine();


        Console.WriteLine("\nEnter start date (dd-MM-yyyy [HH:mm[:ss]]): ");

        var startDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (startDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            Console.WriteLine("Your task has not been added");
            return;
        }

        Console.WriteLine("Enter start date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (endDate is null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            Console.WriteLine("Your task has not been added");
            return;
        }
        var id = taskService.GetLastId() + 1;
        var task = new CalendarEvent(id, name, description, startDate.Value, endDate.Value);
        taskService.Add(task);
    }

    public void GetTasksView()
    {
        Console.WriteLine("What tasks do you want to view");
        Extensions.PrintMenu(menuActionService, MenuType.TaskMenu);

        var key = Console.ReadKey().KeyChar;
        Console.WriteLine();

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
        if (!int.TryParse(Console.ReadLine(), out var year))
        {
            Console.WriteLine("\nGiven year is invalid, try again");
            return;
        }
        Console.WriteLine();
        var tasks = taskService.GetTasksByYear(year);
        PrintTasks(tasks);
    }

    private void ShowTasksByRangeView()
    {
        Console.WriteLine("Enter start date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var startDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (startDate == null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            return;
        }

        Console.WriteLine("Enter end date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var endDate = ParseDate(Console.ReadLine(), formats);
        Console.WriteLine();

        if (endDate == null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
        }

        var tasks = taskService.GetTasksByDateRange(startDate.Value, endDate.Value);
        PrintTasks(tasks);
    }

    private void ShowTasksByDateView()
    {
        Console.WriteLine("Enter start date (dd-MM-yyyy [HH:mm[:ss]]): ");
        var date = ParseDate(Console.ReadLine(), formats);
        if (date == null)
        {
            Console.WriteLine("Could not parse given date to correct format (dd-MM-yyyy [HH:mm[:ss]])");
            return;
        }
        var tasks = taskService.GetTaskByDate(date.Value);
        PrintTasks(tasks);
    }

    private static void PrintTasks(List<CalendarEvent> tasks)
    {
        var table = new ConsoleTable("Name", "Description", "Start Date", "EndDate", "Duration");
        foreach (var task in tasks)
        {
            var duration = task.EndDate - task.StartDate;
            table.AddRow(
                task.Name,
                task.Description,
                task.StartDate.ToString(),
                task.EndDate.ToString(),
                $"{(int)duration.TotalHours}h {duration.Minutes}m"
                );
        }
        table.Write();
    }
    private static DateTime? ParseDate(string date, string[] formats)
    {

        var wasParseSuccesfull = DateTime.TryParseExact(
            date.Trim(), //Trim is used to ensure no leading or trailing spaces could prevent parsing
            formats,
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out var resultDate
            );

        return wasParseSuccesfull ? resultDate : null;
    }

    private void InitializeTaskViewsActionService()
    {
        menuActionService.AddNewAction(1, "Show all tasks", MenuType.TaskMenu);
        menuActionService.AddNewAction(2, "Show tasks by given year", MenuType.TaskMenu);
        menuActionService.AddNewAction(3, "Show tasks between given date range", MenuType.TaskMenu);
        menuActionService.AddNewAction(4, "Show tasks on given day", MenuType.TaskMenu);
        menuActionService.AddNewAction(5, "Cancel", MenuType.TaskMenu);
    }
}
