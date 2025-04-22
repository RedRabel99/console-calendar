using ConsoleCalendar.Domain.Common;
using ConsoleCalendar.Domain.Helpers;

namespace ConsoleCalendar;

public class MenuAction : BaseEnitity
{
    
    public string Name { get; set; }
    public MenuType MenuName { get; set; }

    public MenuAction(int id, string name, MenuType menuName)
    {
        Id = id;
        Name = name;
        MenuName = menuName;
    }
}
