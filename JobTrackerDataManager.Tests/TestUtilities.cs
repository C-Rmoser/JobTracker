using System;
using System.Collections.Generic;
using DataAccess.Models;

namespace JobTrackerDataManager.Tests;

public static class TestUtilities
{
    public static List<JobModel> GetJobs()
    {
        List<JobModel> jobs = new()
        {
            new JobModel()
            {
                Id = 1,
                Title = "C# Developer",
                Location = "Salzburg",
                LinkToDetails = "www.karriere.at/csharpjob",
                Origin = "Karriere.at",
                FirstSeenOn = DateTime.Now
            },
            new JobModel()
            {
                Id = 2,
                Title = "Javascript Developer",
                Location = "Wals",
                LinkToDetails = "www.jobs.at/jsjob",
                Origin = "Jobs.at",
                FirstSeenOn = DateTime.Now
            },
            new JobModel()
            {
                Id = 3,
                Title = "FullStack Developer",
                Location = "Haag",
                LinkToDetails = "www.dev-jobs.at/fullstackjob",
                Origin = "dev-jobs.at",
                FirstSeenOn = DateTime.Now
            }
        };

        return jobs;
    }
}