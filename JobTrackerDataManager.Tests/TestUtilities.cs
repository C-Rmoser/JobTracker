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

    public static IEnumerable<ParseHubRunModel> GetParseHubRuns()
    {
        List<ParseHubRunModel> runs = new()
        {
            new ParseHubRunModel()
            {
                ProjectToken = "tB9RNrweWz1m",
                RunToken = "tTokxBvdDnn_",
                Status = "initialized",
                DataReady = "0",
                StartTime = "2022-08-11T11:51:19",
                EndTime = null,
                Pages = "2",
                Md5Sum = null,
                StartUrl = "https://www.karriere.at/jobs",
                StartTemplate = "main_template",
                StartValue = "{}"
            },

            new ParseHubRunModel()
            {
                ProjectToken = "tB9RNrweWz1m",
                RunToken = "tTokxBvdDnn_",
                Status = "initialized",
                DataReady = "1",
                StartTime = "2022-08-11T11:51:19",
                EndTime = "2022-08-11T11:51:35.824534",
                Pages = "2",
                Md5Sum = "8b01f39c3e7aa2a78380ed3ad52c70f5",
                StartUrl = "https://www.karriere.at/jobs",
                StartTemplate = "main_template",
                StartValue = "{}"
            }
        };

        return runs;
    }
}