using MergeIntervals.Core.Interfaces;

namespace MergeIntervals.Core.Implementations;

/// <summary>
/// Represents an interval with a start and end point.
/// </summary>
public class Interval : IInterval
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Interval"/> class.
    /// </summary>
    /// <param name="start">The start point of the interval.</param>
    /// <param name="end">The end point of the interval.</param>
    /// <exception cref="ArgumentException">Thrown when start is greater than end.</exception>
    public Interval(int start, int end)
    {
        if (start > end)
        {
            throw new ArgumentException("Start cannot be greater than end.", nameof(start));
        }

        Start = start;
        End = end;
    }

    /// <summary>
    /// Gets the start point of the interval.
    /// </summary>
    public int Start { get; }

    /// <summary>
    /// Gets the end point of the interval.
    /// </summary>
    public int End { get; }

    /// <summary>
    /// Returns a string representation of the interval.
    /// </summary>
    /// <returns>A string representation of the interval in the format [start, end].</returns>
    public override string ToString()
    {
        return $"[{Start}, {End}]";
    }

    /// <summary>
    /// Determines whether the specified object is equal to the current interval.
    /// </summary>
    /// <param name="obj">The object to compare with the current interval.</param>
    /// <returns>true if the specified object is equal to the current interval; otherwise, false.</returns>
    public override bool Equals(object? obj)
    {
        return obj is Interval interval && Start == interval.Start && End == interval.End;
    }

    /// <summary>
    /// Serves as the default hash function.
    /// </summary>
    /// <returns>A hash code for the current interval.</returns>
    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }
} 