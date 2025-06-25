using FluentAssertions;
using Microsoft.Extensions.Options;
using MergeIntervals.Core.Configuration;
using MergeIntervals.Core.Implementations;
using MergeIntervals.Core.Interfaces;

namespace MergeIntervals.Tests.Implementations;

/// <summary>
/// Unit tests for the <see cref="IntervalMerger"/> class.
/// </summary>
public class IntervalMergerTests
{
    private readonly IntervalMerger intervalMerger;
    private readonly IntervalMergerOptions defaultOptions;

    public IntervalMergerTests()
    {
        defaultOptions = new IntervalMergerOptions();
        IOptions<IntervalMergerOptions> options = Options.Create(defaultOptions);
        intervalMerger = new IntervalMerger(options);
    }

    [Fact]
    public void Constructor_NullOptions_ThrowsArgumentNullException()
    {
        // Act & Assert
        Action act = () => new IntervalMerger(null!);
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("options");
    }

    [Fact]
    public void Merge_NullIntervals_ThrowsArgumentNullException()
    {
        // Act & Assert
        Action act = () => intervalMerger.Merge(null!);
        act.Should().Throw<ArgumentNullException>()
           .WithParameterName("intervals");
    }

    [Fact]
    public async Task MergeAsync_NullIntervals_ThrowsArgumentNullException()
    {
        // Act & Assert
        Func<Task> act = async () => await intervalMerger.MergeAsync(null!);
        await act.Should().ThrowAsync<ArgumentNullException>()
                 .WithParameterName("intervals");
    }

    [Fact]
    public void Merge_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        List<IInterval> intervals = new();

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task MergeAsync_EmptyList_ReturnsEmptyList()
    {
        // Arrange
        List<IInterval> intervals = new();

        // Act
        IEnumerable<IInterval> result = await intervalMerger.MergeAsync(intervals);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void Merge_SingleInterval_ReturnsSingleInterval()
    {
        // Arrange
        List<IInterval> intervals = new() { new Interval(1, 4) };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().ContainSingle()
              .Which.Should().BeEquivalentTo(new Interval(1, 4));
    }

    [Fact]
    public void Merge_RequirementExample_ReturnsExpectedResult()
    {
        // Arrange - Input: [[1, 4], [2, 6], [8, 10], [15, 18]]
        // Expected: [[1, 6], [8, 10], [15, 18]]
        List<IInterval> intervals = new()
        {
            new Interval(1, 4),
            new Interval(2, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task MergeAsync_RequirementExample_ReturnsExpectedResult()
    {
        // Arrange - Input: [[1, 4], [2, 6], [8, 10], [15, 18]]
        // Expected: [[1, 6], [8, 10], [15, 18]]
        List<IInterval> intervals = new()
        {
            new Interval(1, 4),
            new Interval(2, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        // Act
        IEnumerable<IInterval> result = await intervalMerger.MergeAsync(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Merge_OverlappingIntervals_MergesCorrectly()
    {
        // Arrange
        List<IInterval> intervals = new()
        {
            new Interval(1, 3),
            new Interval(2, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Merge_TouchingIntervals_MergesCorrectly()
    {
        // Arrange - intervals that touch at boundaries should merge
        List<IInterval> intervals = new()
        {
            new Interval(1, 4),
            new Interval(4, 5)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 5)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Merge_NonOverlappingIntervals_ReturnsOriginalIntervals()
    {
        // Arrange
        List<IInterval> intervals = new()
        {
            new Interval(1, 2),
            new Interval(3, 5),
            new Interval(6, 7)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(intervals);
    }

    [Fact]
    public void Merge_UnsortedIntervals_SortsAndMergesCorrectly()
    {
        // Arrange - unsorted input
        List<IInterval> intervals = new()
        {
            new Interval(15, 18),
            new Interval(1, 4),
            new Interval(8, 10),
            new Interval(2, 6)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Merge_AllOverlappingIntervals_ReturnsSingleInterval()
    {
        // Arrange
        List<IInterval> intervals = new()
        {
            new Interval(1, 4),
            new Interval(2, 5),
            new Interval(3, 6),
            new Interval(4, 7)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 7)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Merge_ExceedsMaxIntervals_ThrowsArgumentException()
    {
        // Arrange
        IntervalMergerOptions options = new() { MaxIntervals = 2 };
        IOptions<IntervalMergerOptions> optionsWrapper = Options.Create(options);
        IntervalMerger merger = new(optionsWrapper);

        List<IInterval> intervals = new()
        {
            new Interval(1, 2),
            new Interval(3, 4),
            new Interval(5, 6)
        };

        // Act & Assert
        Action act = () => merger.Merge(intervals);
        act.Should().Throw<ArgumentException>()
           .WithMessage("Number of intervals exceeds maximum allowed (2). (Parameter 'intervals')");
    }

    [Theory]
    [InlineData(-10, -5, -4, -1)] // Negative intervals - no overlap
    [InlineData(0, 0, 1, 1)] // Zero-length intervals - no overlap
    public void Merge_EdgeCases_NoOverlap_WorksCorrectly(int start1, int end1, int start2, int end2)
    {
        // Arrange
        List<IInterval> intervals = new()
        {
            new Interval(start1, end1),
            new Interval(start2, end2)
        };

        List<IInterval> expected = new()
        {
            new Interval(start1, end1),
            new Interval(start2, end2)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public void Merge_EdgeCases_LargeNumbers_WorksCorrectly()
    {
        // Arrange - intervals that DO overlap
        List<IInterval> intervals = new()
        {
            new Interval(100, 200),
            new Interval(150, 300)
        };

        List<IInterval> expected = new()
        {
            new Interval(100, 300)
        };

        // Act
        IEnumerable<IInterval> result = intervalMerger.Merge(intervals);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [Fact]
    public async Task Merge_ThreadSafety_MultipleThreads_WorksCorrectly()
    {
        // Arrange
        List<IInterval> intervals = new()
        {
            new Interval(1, 4),
            new Interval(2, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        List<IInterval> expected = new()
        {
            new Interval(1, 6),
            new Interval(8, 10),
            new Interval(15, 18)
        };

        // Act - run multiple times concurrently
        List<Task<IEnumerable<IInterval>>> tasks = new();
        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => intervalMerger.Merge(intervals)));
        }

        await Task.WhenAll(tasks);

        // Assert - all results should be the same
        foreach (Task<IEnumerable<IInterval>> task in tasks)
        {
            IEnumerable<IInterval> result = await task;
            result.Should().BeEquivalentTo(expected);
        }
    }
} 