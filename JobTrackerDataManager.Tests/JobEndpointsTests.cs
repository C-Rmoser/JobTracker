using System.Collections.Generic;
using DataAccess.Data;
using DataAccess.Models;
using FluentAssertions;
using JobTrackerDataManager.Endpoints;
using NSubstitute;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class JobEndpointsTests
{
    private readonly IJobData _data = Substitute.For<IJobData>();

    [Fact]
    public async void GetJobs_ReturnsJobs_WhenJobsExist()
    {
        // Arrange
        var jobs = TestUtilities.GetJobs();

        _data.GetJobs().Returns(jobs);

        // Act
        var result = await JobEndpoints.GetJobs(_data);

        // Assert
        result.GetOkObjectResultValue<List<JobModel>>()!.Count.Should().Be(jobs.Count);
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void GetJobs_ReturnsEmptyList_WhenEmpty()
    {
        // Arrange
        _data.GetJobs().Returns(new List<JobModel>());

        // Act
        var result = await JobEndpoints.GetJobs(_data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void GetJobById_ReturnsJob_WhenJobExists()
    {
        // Arrange
        const int id = 1;
        var job = TestUtilities.GetJobs()[0];

        _data.GetJobById(Arg.Is(id)).Returns(job);

        // Act
        var result = await JobEndpoints.GetJobById(id, _data);

        // Assert
        result.GetOkObjectResultValue<JobModel>().Should().BeEquivalentTo(job);
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void GetJobById_ReturnsNotFound_WhenJobDoesNotExist()
    {
        // Arrange
        _data.GetJobById(Arg.Any<int>()).Returns((JobModel?) null);

        // Act
        var result = await JobEndpoints.GetJobById(1, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(404);
    }

    [Fact]
    public async void InsertJob_ReturnsOk_WhenArgumentIsValid()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        await _data.InsertJob(Arg.Is(job));

        // Act
        var result = await JobEndpoints.InsertJob(job, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void InsertJobs_ReturnsOk_WhenArgumentIsValid()
    {
        // Arrange
        var jobs = TestUtilities.GetJobs();
        await _data.InsertJobs(Arg.Is(jobs));

        // Act
        var result = await JobEndpoints.InsertJobs(jobs, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void UpdateJob_ReturnsOk_WhenJobExists()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _data.UpdateJob(Arg.Is(job)).Returns(1);

        // Act
        var result = await JobEndpoints.UpdateJob(job, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void UpdateJob_ReturnsNotFound_WhenJobDoesNotExist()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _data.UpdateJob(Arg.Is(job)).Returns(0);

        // Act
        var result = await JobEndpoints.UpdateJob(job, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(404);
    }

    [Fact]
    public async void ArchiveJob_ReturnsOk_WhenJobExists()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _data.ArchiveJob(Arg.Is(job.Id)).Returns(1);

        // Act
        var result = await JobEndpoints.InsertJob(job, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(200);
    }

    [Fact]
    public async void ArchiveJob_ReturnsNotFound_WhenJobDoesNotExist()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _data.ArchiveJob(Arg.Is(job.Id)).Returns(0);

        // Act
        var result = await JobEndpoints.ArchiveJob(job.Id, _data);

        // Assert
        result.GetOkObjectResultStatusCode().Should().Be(404);
    }
}