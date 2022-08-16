using System.Collections.Generic;
using FluentAssertions;
using JobTrackerDataManager.DTOs;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class UtilitiesTests
{
    [Fact]
    public static void MapKarriereAtDtosToJobs_ShouldReturnJobs_WhenArgumentIsValid()
    {
        // Arrange
        var dtos = Utilities.GetKarriereAtDtos();

        // Act
        var result = JobTrackerDataManager.Utilities.Utilities.MapKarriereAtDtosToJobs(dtos);

        // Assert
        result.Count.Should().Be(dtos.Count);
    }

    [Fact]
    public static void MapDevJobsAtDtosToJobs_ShouldReturnJobs_WhenArgumentIsValid()
    {
        // Arrange
        var dtos = Utilities.GetDevJobsAtDtos();

        // Act
        var result = JobTrackerDataManager.Utilities.Utilities.MapDevJobsAtDtosToJobs(dtos);

        // Assert
        result.Count.Should().Be(dtos.Count);
    }

    [Fact]
    public static void MapJobsAtDtosToJobs_ShouldReturnJobs_WhenArgumentIsValid()
    {
        // Arrange
        var dtos = Utilities.GetJobsAtDtos();

        // Act
        var result = JobTrackerDataManager.Utilities.Utilities.MapJobsAtDtosToJobs(dtos);

        // Assert
        result.Count.Should().Be(dtos.Count);
    }

    [Fact]
    public static void MapKarriereAtDtosToJobs_ShouldReturnEmptyList_WhenArgumentIsEmpty()
    {
        // Arrange
        var dtos = new List<KarriereAtDto>();

        // Act
        var result = JobTrackerDataManager.Utilities.Utilities.MapKarriereAtDtosToJobs(dtos);

        // Assert
        result.Count.Should().Be(dtos.Count);
    }

    [Fact]
    public static void MapDevJobsAtDtosToJobs_ShouldReturnEmptyList_WhenArgumentIsEmpty()
    {
        // Arrange
        var dtos = new List<DevJobsDto>();

        // Act
        var result = JobTrackerDataManager.Utilities.Utilities.MapDevJobsAtDtosToJobs(dtos);

        // Assert
        result.Count.Should().Be(dtos.Count);
    }

    [Fact]
    public static void MapJobsAtDtosToJobs_ShouldReturnEmptyList_WhenArgumentIsEmpty()
    {
        // Arrange
        var dtos = new List<JobsAtDto>();

        // Act
        var result = JobTrackerDataManager.Utilities.Utilities.MapJobsAtDtosToJobs(dtos);

        // Assert
        result.Count.Should().Be(dtos.Count);
    }
}