using ConsoleCalendar.App.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Domain.Helpers;

namespace TaskManager;

public class MenuActionService : IMenuActionService
{
    private List<MenuAction> menuActions { get; set; }
    public MenuActionService()
    {
        menuActions = new List<MenuAction>();
    }

    public void AddNewAction(int id, string name, MenuType menuName)
    {
        var menuAction = new MenuAction(id, name, menuName);
        menuActions.Add(menuAction);
    }

    public List<MenuAction> GetMenuActionsByMenuName(MenuType menuName)
    {
        return menuActions.Where(m => m.MenuName == menuName).ToList();
    }
}
