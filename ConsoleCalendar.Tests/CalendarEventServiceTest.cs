using ConsoleCalendar.App.Abstract;
using FluentAssertions;

namespace ConsoleCalendar.Tests;

public class CalendarEventServiceTest
{
    [Fact]
    public void CanGetAllCallendarEvents()
    {
        //Arrange
        var service = CreateCalendarEventServiceWithSampleData();
        //Act
        var calendarEvents = service.GetAllCalendarEvents();

        //Assert
        calendarEvents.Should().BeOfType(typeof(List<CalendarEvent>));
        calendarEvents.Should().NotBeNull();
        calendarEvents.Should().HaveCount(5);
    }

    [Fact]
    public void CanGetCallendarEventsByYear()
    {
        //Arrange
        var service = CreateCalendarEventServiceWithSampleData();
        //Act
        var calendarEventsOf2025 = service.GetCalendarEventsByYear(2025);
        var calendarEventsOf2027 = service.GetCalendarEventsByYear(2027);
        var calendarEventsOf2028 = service.GetCalendarEventsByYear(2028);

        //Assert
        calendarEventsOf2025.Should().BeOfType(typeof(List<CalendarEvent>));
        calendarEventsOf2025.Should().NotBeNull();
        calendarEventsOf2025.Should().HaveCount(3);

        calendarEventsOf2027.Should().BeOfType(typeof(List<CalendarEvent>));
        calendarEventsOf2027.Should().NotBeNull();
        calendarEventsOf2027.Should().HaveCount(2);

        calendarEventsOf2028.Should().BeOfType(typeof(List<CalendarEvent>));
        calendarEventsOf2028.Should().NotBeNull();
        calendarEventsOf2028.Should().HaveCount(0);
    }

    [Fact]
    public void CanGetCalendarEventById()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();

        // Act
        var calendarEventOfId1 = service.Get(1);
        var calendarEventOfId2 = service.Get(2);

        // Assert
        calendarEventOfId1.Should().NotBeNull();
        calendarEventOfId1.Name.Should().Be("Event");
        calendarEventOfId2.Should().NotBeNull();
        calendarEventOfId2.Name.Should().Be("Event 2");
    }

    [Fact]
    public void CanRemoveCalendarEvent()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();

        // Act
        var wasRemoved = service.Remove(1);
        var eventAfterRemoval = service.Get(1);

        // Assert
        wasRemoved.Should().BeTrue();
        eventAfterRemoval.Should().BeNull();
    }

    [Fact]
    public void ReturnsFalseWhenRemovingNonExistentEvent()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();

        // Act
        var result = service.Remove(999);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void CanUpdateCalendarEvent()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();
        var updatedEvent = new CalendarEvent(1, "Updated Event", "Updated Description",
            new DateTime(2025, 01, 01), new DateTime(2025, 01, 02));

        // Act
        var wasUpdated = service.Update(1, updatedEvent);
        var fetchedEvent = service.Get(1);

        // Assert
        wasUpdated.Should().BeTrue();
        fetchedEvent.Name.Should().Be("Updated Event");
        fetchedEvent.Description.Should().Be("Updated Description");
    }

    [Fact]
    public void ReturnsFalseWhenUpdatingNonExistentEvent()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();
        var calendarEvent = new CalendarEvent(999, "Doesn't exist", "Desc", DateTime.Now, DateTime.Now);

        // Act
        var result = service.Update(999, calendarEvent);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void CanGetCalendarEventsByDateRange()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();

        // Act
        var eventsInRange = service.GetCalendarEventsByDateRange(
            new DateTime(2025, 09, 01),
            new DateTime(2025, 10, 01)
        );

        // Assert
        eventsInRange.Should().HaveCount(1);
    }

    [Fact]
    public void GetLastIdShouldReturnHighestCalendarEventId()
    {
        // Arrange
        var service = CreateCalendarEventServiceWithSampleData();

        // Act
        var lastId = service.GetLastId();

        // Assert
        lastId.Should().Be(5);
    }

    [Fact]
    public void GetLastIdShouldReturnZeroWhenNoEventsExist()
    {
        // Arrange
        var service = new CalendarEventService();

        // Act
        var lastId = service.GetLastId();

        // Assert
        lastId.Should().Be(0);
    }



    private ICalendarEventService CreateCalendarEventServiceWithSampleData()
    {
        CalendarEvent calendarEvent1 = new CalendarEvent(
            1,
            "Event",
            "Event description",
            new DateTime(2025, 09, 01),
            new DateTime(2025, 09, 05));
        CalendarEvent calendarEvent2 = new CalendarEvent(
            2,
            "Event 2",
            "Event description 2",
            new DateTime(2025, 10, 01, 20, 30, 0),
            new DateTime(2025, 10, 01, 21, 30, 0));

        CalendarEvent calendarEvent3 = new CalendarEvent(
            3,
            "Event 3",
            "Event description 3",
            new DateTime(2025, 12, 01, 15, 30, 0),
            new DateTime(2025, 12, 05, 20, 0, 0));
        CalendarEvent calendarEvent4 = new CalendarEvent(
            4,
            "Event 3",
            "Event description 3",
            new DateTime(2026, 12, 01, 15, 30, 0),
            new DateTime(2027, 12, 05, 20, 0, 0));
        CalendarEvent calendarEvent5 = new CalendarEvent(
            5,
            "Event 5",
            "Event description 5",
            new DateTime(2027, 12, 01),
            new DateTime(2027, 12, 05));

        ICalendarEventService calendarEventService = new CalendarEventService();
        calendarEventService.Add(calendarEvent1);
        calendarEventService.Add(calendarEvent2);
        calendarEventService.Add(calendarEvent3);
        calendarEventService.Add(calendarEvent4);
        calendarEventService.Add(calendarEvent5);

        return calendarEventService;
    }
}
