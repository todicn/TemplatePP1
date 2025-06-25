namespace MergeIntervals.Core.Interfaces;

/// <summary>
/// Defines the contract for merging overlapping intervals.
/// </summary>
public interface IIntervalMerger
{
    /// <summary>
    /// Merges overlapping intervals from the provided collection.
    /// </summary>
    /// <param name="intervals">The collection of intervals to merge.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the merged intervals.</returns>
    /// <exception cref="ArgumentNullException">Thrown when intervals is null.</exception>
    Task<IEnumerable<IInterval>> MergeAsync(IEnumerable<IInterval> intervals);

    /// <summary>
    /// Merges overlapping intervals from the provided collection synchronously.
    /// </summary>
    /// <param name="intervals">The collection of intervals to merge.</param>
    /// <returns>The merged intervals.</returns>
    /// <exception cref="ArgumentNullException">Thrown when intervals is null.</exception>
    IEnumerable<IInterval> Merge(IEnumerable<IInterval> intervals);
} 