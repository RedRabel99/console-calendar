using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager;

public class MenuAction(int id, string name, string menuName)
{
    public int Id { get; set; } = id;
    public string Name { get; set; } = name;
    public string MenuName { get; set; } = menuName;
}
