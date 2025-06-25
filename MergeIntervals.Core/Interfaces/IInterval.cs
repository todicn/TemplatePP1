namespace MergeIntervals.Core.Interfaces;

/// <summary>
/// Represents an interval with a start and end point.
/// </summary>
public interface IInterval
{
    /// <summary>
    /// Gets the start point of the interval.
    /// </summary>
    int Start { get; }

    /// <summary>
    /// Gets the end point of the interval.
    /// </summary>
    int End { get; }
} 