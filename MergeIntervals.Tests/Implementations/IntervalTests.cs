using FluentAssertions;
using MergeIntervals.Core.Implementations;

namespace MergeIntervals.Tests.Implementations;

/// <summary>
/// Unit tests for the <see cref="Interval"/> class.
/// </summary>
public class IntervalTests
{
    [Fact]
    public void Constructor_ValidInterval_SetsPropertiesCorrectly()
    {
        // Arrange
        const int start = 1;
        const int end = 4;

        // Act
        Interval interval = new(start, end);

        // Assert
        interval.Start.Should().Be(start);
        interval.End.Should().Be(end);
    }

    [Fact]
    public void Constructor_StartEqualsEnd_SetsPropertiesCorrectly()
    {
        // Arrange
        const int start = 5;
        const int end = 5;

        // Act
        Interval interval = new(start, end);

        // Assert
        interval.Start.Should().Be(start);
        interval.End.Should().Be(end);
    }

    [Fact]
    public void Constructor_StartGreaterThanEnd_ThrowsArgumentException()
    {
        // Arrange
        const int start = 10;
        const int end = 5;

        // Act & Assert
        Action act = () => new Interval(start, end);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Start cannot be greater than end. (Parameter 'start')");
    }

    [Fact]
    public void ToString_ReturnsCorrectFormat()
    {
        // Arrange
        Interval interval = new(1, 4);

        // Act
        string result = interval.ToString();

        // Assert
        result.Should().Be("[1, 4]");
    }

    [Fact]
    public void Equals_SameIntervals_ReturnsTrue()
    {
        // Arrange
        Interval interval1 = new(1, 4);
        Interval interval2 = new(1, 4);

        // Act & Assert
        interval1.Equals(interval2).Should().BeTrue();
        interval1.Equals((object)interval2).Should().BeTrue();
    }

    [Fact]
    public void Equals_DifferentIntervals_ReturnsFalse()
    {
        // Arrange
        Interval interval1 = new(1, 4);
        Interval interval2 = new(2, 6);

        // Act & Assert
        interval1.Equals(interval2).Should().BeFalse();
        interval1.Equals((object)interval2).Should().BeFalse();
    }

    [Fact]
    public void Equals_NullObject_ReturnsFalse()
    {
        // Arrange
        Interval interval = new(1, 4);

        // Act & Assert
        interval.Equals(null).Should().BeFalse();
    }

    [Fact]
    public void Equals_DifferentType_ReturnsFalse()
    {
        // Arrange
        Interval interval = new(1, 4);
        string notAnInterval = "not an interval";

        // Act & Assert
        interval.Equals(notAnInterval).Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_SameIntervals_ReturnsSameHashCode()
    {
        // Arrange
        Interval interval1 = new(1, 4);
        Interval interval2 = new(1, 4);

        // Act & Assert
        interval1.GetHashCode().Should().Be(interval2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_DifferentIntervals_ReturnsDifferentHashCodes()
    {
        // Arrange
        Interval interval1 = new(1, 4);
        Interval interval2 = new(2, 6);

        // Act & Assert
        interval1.GetHashCode().Should().NotBe(interval2.GetHashCode());
    }

    [Theory]
    [InlineData(int.MinValue, int.MaxValue)]
    [InlineData(-1000, 1000)]
    [InlineData(0, 0)]
    public void Constructor_EdgeCases_WorksCorrectly(int start, int end)
    {
        // Act
        Interval interval = new(start, end);

        // Assert
        interval.Start.Should().Be(start);
        interval.End.Should().Be(end);
    }
} 