using System.Diagnostics;
using Microsoft.Extensions.Options;
using MergeIntervals.Core.Configuration;
using MergeIntervals.Core.Interfaces;

namespace MergeIntervals.Core.Implementations;

/// <summary>
/// Provides functionality to merge overlapping intervals.
/// </summary>
public class IntervalMerger : IIntervalMerger
{
    private readonly IntervalMergerOptions options;
    private readonly object lockObject = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="IntervalMerger"/> class.
    /// </summary>
    /// <param name="options">The configuration options for the interval merger.</param>
    /// <exception cref="ArgumentNullException">Thrown when options is null.</exception>
    public IntervalMerger(IOptions<IntervalMergerOptions> options)
    {
        this.options = options?.Value ?? throw new ArgumentNullException(nameof(options));
    }

    /// <summary>
    /// Merges overlapping intervals from the provided collection asynchronously.
    /// </summary>
    /// <param name="intervals">The collection of intervals to merge.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the merged intervals.</returns>
    /// <exception cref="ArgumentNullException">Thrown when intervals is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the number of intervals exceeds the maximum allowed.</exception>
    public async Task<IEnumerable<IInterval>> MergeAsync(IEnumerable<IInterval> intervals)
    {
        if (intervals == null)
        {
            throw new ArgumentNullException(nameof(intervals));
        }

        return await Task.Run(() => Merge(intervals));
    }

    /// <summary>
    /// Merges overlapping intervals from the provided collection synchronously.
    /// </summary>
    /// <param name="intervals">The collection of intervals to merge.</param>
    /// <returns>The merged intervals.</returns>
    /// <exception cref="ArgumentNullException">Thrown when intervals is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the number of intervals exceeds the maximum allowed.</exception>
    public IEnumerable<IInterval> Merge(IEnumerable<IInterval> intervals)
    {
        if (intervals == null)
        {
            throw new ArgumentNullException(nameof(intervals));
        }

        lock (lockObject)
        {
            return MergeInternal(intervals);
        }
    }

    /// <summary>
    /// Internal method to perform the actual merging logic.
    /// </summary>
    /// <param name="intervals">The collection of intervals to merge.</param>
    /// <returns>The merged intervals.</returns>
    private IEnumerable<IInterval> MergeInternal(IEnumerable<IInterval> intervals)
    {
        Stopwatch? stopwatch = null;
        if (options.EnablePerformanceLogging)
        {
            stopwatch = Stopwatch.StartNew();
        }

        try
        {
            List<IInterval> intervalList = intervals.ToList();

            if (intervalList.Count == 0)
            {
                return new List<IInterval>();
            }

            if (intervalList.Count > options.MaxIntervals)
            {
                throw new ArgumentException($"Number of intervals exceeds maximum allowed ({options.MaxIntervals}).", nameof(intervals));
            }

            // Sort intervals by start time
            List<IInterval> sortedIntervals = options.UseParallelProcessing && intervalList.Count >= options.ParallelProcessingThreshold
                ? intervalList.AsParallel().OrderBy(i => i.Start).ToList()
                : intervalList.OrderBy(i => i.Start).ToList();

            List<IInterval> merged = new();
            IInterval current = sortedIntervals[0];

            for (int i = 1; i < sortedIntervals.Count; i++)
            {
                IInterval next = sortedIntervals[i];
                
                // Check if current interval overlaps with next interval
                if (current.End >= next.Start)
                {
                    // Merge the intervals
                    current = new Interval(current.Start, Math.Max(current.End, next.End));
                }
                else
                {
                    // No overlap, add current to result and move to next
                    merged.Add(current);
                    current = next;
                }
            }

            // Add the last interval
            merged.Add(current);

            return merged;
        }
        finally
        {
            if (stopwatch != null && options.EnablePerformanceLogging)
            {
                stopwatch.Stop();
                // In a real application, you would log this to a proper logging framework
                Console.WriteLine($"Interval merging completed in {stopwatch.ElapsedMilliseconds}ms");
            }
        }
    }
} 