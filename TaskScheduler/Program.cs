namespace TaskManager;

internal class Program
{
    static void Main(string[] args)
    {
        var taskService = new TaskService();
        //seed initial data
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

        var app = new App(taskService);
        
        app.Run();
    }
}
