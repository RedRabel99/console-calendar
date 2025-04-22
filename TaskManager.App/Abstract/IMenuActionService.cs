using ConsoleCalendar.Domain.Helpers;

namespace ConsoleCalendar.App.Abstract;

public  interface IMenuActionService
{
    public void AddNewAction(int id, string name, MenuType menuName);
    public List<MenuAction> GetMenuActionsByMenuName(MenuType menuName);
}
