namespace MergeIntervals.Core.Configuration;

/// <summary>
/// Configuration options for the interval merger.
/// </summary>
public class IntervalMergerOptions
{
    /// <summary>
    /// The configuration section name for interval merger options.
    /// </summary>
    public const string SectionName = "IntervalMerger";

    /// <summary>
    /// Gets or sets a value indicating whether to enable performance logging.
    /// </summary>
    public bool EnablePerformanceLogging { get; set; } = false;

    /// <summary>
    /// Gets or sets the maximum number of intervals that can be processed in a single operation.
    /// </summary>
    public int MaxIntervals { get; set; } = 10000;

    /// <summary>
    /// Gets or sets a value indicating whether to use parallel processing for large collections.
    /// </summary>
    public bool UseParallelProcessing { get; set; } = true;

    /// <summary>
    /// Gets or sets the threshold for switching to parallel processing.
    /// </summary>
    public int ParallelProcessingThreshold { get; set; } = 1000;
} 