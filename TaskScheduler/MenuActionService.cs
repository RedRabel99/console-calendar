using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class MenuActionService
{
    private List<MenuAction> menuActions { get; set; }
    public MenuActionService()
    {
        menuActions = new List<MenuAction>();
    }

    public void AddNewAction(int i, string name, MenuTypes menuName)
    {
        var menuAction = new MenuAction(i, name, menuName);
        menuActions.Add(menuAction);
    }

    public List<MenuAction> GetMenuActionsByMenuName(MenuTypes menuName)
    {
        return menuActions.Where(m => m.MenuName == menuName).ToList();
    }
}
