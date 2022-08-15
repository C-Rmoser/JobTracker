using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Data;
using DataAccess.DbAccess;
using DataAccess.Models;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace JobTrackerDataManager.Tests;

public class JobDataTests
{
    private readonly ISqlDataAccess _db = Substitute.For<ISqlDataAccess>();

    [Fact]
    public async void GetJobs_ReturnsJobs_WhenJobsExist()
    {
        // Arrange
        IEnumerable<JobModel> jobs = TestUtilities.GetJobs();
        _db.LoadData<JobModel, dynamic>(Arg.Any<string>(), Arg.Any<Object>()).Returns(jobs);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.GetJobs();

        // Assert
        result.Should().BeEquivalentTo(jobs);
    }

    [Fact]
    public async void GetJobs_ReturnsEmptyList_WhenJobsDoNotExist()
    {
        // Arrange
        IEnumerable<JobModel> jobs = new List<JobModel>();
        _db.LoadData<JobModel, dynamic>(Arg.Any<string>(), Arg.Any<Object>()).Returns(jobs);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.GetJobs();

        // Assert
        result.Count().Should().Be(0);
    }

    [Fact]
    public async void GetJobById_ReturnsJob_WhenJobExists()
    {
        // Arrange
        var jobs = new List<JobModel>();
        var job = TestUtilities.GetJobs()[0];
        jobs.Add(job);

        _db.LoadData<JobModel, dynamic>(Arg.Any<string>(), Arg.Any<Object>()).Returns(jobs);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.GetJobById(1);

        // Assert
        result.Should().Be(job);
    }

    [Fact]
    public async void GetJobById_ReturnsNull_WhenJobDoesNotExist()
    {
        // Arrange
        _db.LoadData<JobModel, dynamic>(Arg.Any<string>(), Arg.Any<Object>()).Returns(new List<JobModel>());
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.GetJobById(1);

        // Assert
        result.Should().Be(null);
    }

    [Fact]
    public async void InsertJob_Returns_1_OnSuccess()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _db.SaveData(Arg.Any<string>(), Arg.Any<Object>()).Returns(1);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.InsertJob(job);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async void InsertJobs_Returns_1_OnSuccess()
    {
        // Arrange
        var jobs = TestUtilities.GetJobs();
        _db.SaveData(Arg.Any<string>(), Arg.Any<Object>()).Returns(1);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.InsertJobs(jobs);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async void UpdateJob_Returns_1_WhenJobExists()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _db.SaveData(Arg.Any<string>(), Arg.Any<Object>()).Returns(1);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.InsertJob(job);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async void UpdateJob_Returns_0_WhenJobDoesNotExist()
    {
        // Arrange
        var job = TestUtilities.GetJobs()[0];
        _db.SaveData(Arg.Any<string>(), Arg.Any<Object>()).Returns(0);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.InsertJob(job);

        // Assert
        result.Should().Be(0);
    }

    [Fact]
    public async void ArchiveJob_Returns_1_WhenJobExists()
    {
        // Arrange
        _db.SaveData(Arg.Any<string>(), Arg.Any<Object>()).Returns(1);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.ArchiveJob(0);

        // Assert
        result.Should().Be(1);
    }

    [Fact]
    public async void ArchiveJob_Returns_0_WhenJobDoesNotExist()
    {
        // Arrange
        _db.SaveData(Arg.Any<string>(), Arg.Any<Object>()).Returns(0);
        var jobData = new JobData(_db);

        // Act
        var result = await jobData.ArchiveJob(0);

        // Assert
        result.Should().Be(0);
    }
}