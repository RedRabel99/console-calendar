namespace TaskManager;

internal class Program
{
    static void Main(string[] args)
    {
        var taskService = new TaskService();
        var task1 = new Task(
            "Work meeting",
            "",
            new DateTime(2025, 4, 1, 15, 0, 0),
            new DateTime(2025, 4, 1, 15, 30, 0)
        );

        var task2 = new Task(
            "Holidays",
            "Holidays with parents",
            new DateTime(2025, 6, 25),
            new DateTime(2025, 7, 4)
        );

        taskService.Add(task1);
        taskService.Add(task2);

        var menuActionService = new MenuActionService();
        menuActionService.Initialize();

        Console.WriteLine("Welcome to Task Manager\n");

        while (true)
        {
            Console.WriteLine("Please enter what action do you want to take:");

            foreach(var menuAction in menuActionService.GetMenuActionsByMenuName("Main"))
            {
                Console.WriteLine($"{menuAction.Id}. {menuAction.Name}");
            }
            var operation = Console.ReadKey();

            switch (operation.KeyChar)
            {
                case '1':
                    Console.WriteLine("a");
                    break;
                case '2':
                    Console.WriteLine("b");
                    break;
                case '3':
                    return;
                default:
                    Console.WriteLine("Entered action does not exist");
                    break;
            }
        }
    }

    
}
