using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.App.Abstract
{
    public interface ITaskService<T>
    {
        List<T> Tasks { get; }

        List<T> GetAllTasks();
        int GetLastId();
        List<T> GetTasksByDateRange(DateTime startTime, DateTime endtime);
        List<T> GetTasksByYear(int year);
        List<T> GetTaskByDate(DateTime date);
    }
}
