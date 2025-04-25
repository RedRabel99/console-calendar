using ConsoleCalendar.Domain.Helpers;
using FluentAssertions;
using System;
using Xunit;

namespace ConsoleCalendar.Tests;

public class CalendarEventTest
{
    [Fact]
    public void CanCreateCalendarEventWithValidDates()
    {
        // Arrange
        var start = new DateTime(2025, 5, 1);
        var end = new DateTime(2025, 5, 2);

        // Act
        var calendarEvent = new CalendarEvent(1, "Event", "Description", start, end);

        // Assert
        calendarEvent.Id.Should().Be(1);
        calendarEvent.Name.Should().Be("Event");
        calendarEvent.Description.Should().Be("Description");
        calendarEvent.StartDate.Should().Be(start);
        calendarEvent.EndDate.Should().Be(end);
    }

    [Fact]
    public void ThrowsExceptionWhenStartDateIsAfterEndDate()
    {
        // Arrange
        var start = new DateTime(2025, 6, 10);
        var end = new DateTime(2025, 6, 5);

        // Act
        Action act = () => new CalendarEvent(1, "Invalid", "Should fail", start, end);

        // Assert
        act.Should().Throw<InvalidDateRangeException>()
            .WithMessage("End date cannot be before start date.");
    }

    [Fact]
    public void CanCreateEventWithSameStartAndEndDate()
    {
        // Arrange
        var date = new DateTime(2025, 7, 1);

        // Act
        var calendarEvent = new CalendarEvent(2, "One-day Event", "Same day", date, date);

        // Assert
        calendarEvent.StartDate.Should().Be(date);
        calendarEvent.EndDate.Should().Be(date);
    }
}
