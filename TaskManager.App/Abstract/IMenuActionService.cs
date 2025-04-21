using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager;
using TaskManager.Domain.Helpers;

namespace ConsoleCalendar.App.Abstract
{
    public  interface IMenuActionService
    {
        public void AddNewAction(int id, string name, MenuType menuName);
        public List<MenuAction> GetMenuActionsByMenuName(MenuType menuName);
    }
}
