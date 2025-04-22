using ConsoleCalendar.App.Abstract;
using ConsoleCalendar.Domain.Helpers;

namespace ConsoleCalendar;

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
