using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public static class Extensions
{
    public static bool Between(this DateTime date, DateTime start, DateTime end, bool inclusive = true)
    {
        if (inclusive)
        {
            return date >= start && date <= end;
        }

        return date >= start && date <= end;
    }

    public static void Initialize(this MenuActionService menuActionService)
    {
        menuActionService.AddNewAction(1, "Add Task", "Main");
        menuActionService.AddNewAction(2, "Show Task", "Main");
        menuActionService.AddNewAction(3, "Exit", "Main");
    }
}
