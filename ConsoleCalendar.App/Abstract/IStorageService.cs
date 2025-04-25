using ConsoleCalendar.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCalendar.App.Abstract
{
    public interface IStorageService<T> where T : BaseEnitity
    {
        public List<T> Load();
        public void Save(List<T> data);

    }
}
