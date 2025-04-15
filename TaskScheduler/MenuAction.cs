using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class MenuAction(int id, string name, MenuTypes menuName)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public MenuTypes MenuName { get; set; } = menuName;
}
