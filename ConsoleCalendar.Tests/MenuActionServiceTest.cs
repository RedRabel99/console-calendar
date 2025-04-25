using ConsoleCalendar;
using ConsoleCalendar.Domain.Helpers;
using FluentAssertions;

namespace ConsoleCalendar.Tests;

public class MenuActionServiceTests
{
    [Fact]
    public void CanAddNewAction()
    {
        // Arrange
        var service = new MenuActionService();

        // Act
        service.AddNewAction(1, "Add Event", MenuType.MainMenu);
        var actions = service.GetMenuActionsByMenuName(MenuType.MainMenu);

        // Assert
        actions.Should().HaveCount(1);
        actions[0].Id.Should().Be(1);
        actions[0].Name.Should().Be("Add Event");
        actions[0].MenuName.Should().Be(MenuType.MainMenu);
    }

    [Fact]
    public void CanFilterActionsByMenuType()
    {
        // Arrange
        var service = new MenuActionService();
        service.AddNewAction(1, "Show Events", MenuType.TaskMenu);
        service.AddNewAction(2, "Delete Event", MenuType.TaskMenu);
        service.AddNewAction(3, "Exit", MenuType.MainMenu);

        // Act
        var taskMenuActions = service.GetMenuActionsByMenuName(MenuType.TaskMenu);

        // Assert
        taskMenuActions.Should().HaveCount(2);
        taskMenuActions.All(a => a.MenuName == MenuType.TaskMenu).Should().BeTrue();
    }

    [Fact]
    public void ReturnsEmptyListWhenNoActionsForMenuType()
    {
        // Arrange
        var service = new MenuActionService();

        // Act
        var result = service.GetMenuActionsByMenuName(MenuType.TaskMenu);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public void CanAddMultipleMenuActions()
    {
        // Arrange
        var service = new MenuActionService();

        // Act
        service.AddNewAction(1, "Add", MenuType.MainMenu);
        service.AddNewAction(2, "Edit", MenuType.MainMenu);
        service.AddNewAction(3, "Remove", MenuType.TaskMenu);

        // Assert
        var mainMenu = service.GetMenuActionsByMenuName(MenuType.MainMenu);
        var taskMenu = service.GetMenuActionsByMenuName(MenuType.TaskMenu);

        mainMenu.Should().HaveCount(2);
        taskMenu.Should().HaveCount(1);
    }
}
