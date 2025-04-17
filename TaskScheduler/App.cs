using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class App
{
    private readonly TaskService taskService;
    private readonly MenuActionService menuActionService;

    public App(TaskService taskService)
    {
        this.taskService = taskService;
        this.menuActionService = new MenuActionService();
        InitializeMainMenuActionService();
    }
    public void Run()
    {
        var taskViews = new TaskViews(taskService);
        Console.WriteLine("Welcome to Task Manager\n");

        while (true)
        {
            Console.WriteLine("Please enter what action do you want to take:");

            foreach (var menuAction in menuActionService.GetMenuActionsByMenuName(MenuTypes.MainMenu))
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
            }
            var operation = Console.ReadKey();
            Console.WriteLine();

            switch (operation.KeyChar)
            {
                case '1':
                    taskViews.AddTaskView();
                    break;
                case '2':
                    taskViews.GetTasksView();
                    break;
                case '3':
                    return;
                default:
                    Console.WriteLine("Entered action does not exist");
                    break;
            }
        }
    }

    private void InitializeMainMenuActionService()
    {
        menuActionService.AddNewAction(1, "Add Task", MenuTypes.MainMenu);
        menuActionService.AddNewAction(2, "Show Task", MenuTypes.MainMenu);
        menuActionService.AddNewAction(3, "Exit", MenuTypes.MainMenu);
    }
}
