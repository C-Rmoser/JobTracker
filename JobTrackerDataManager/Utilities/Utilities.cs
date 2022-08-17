using System.Text.RegularExpressions;
using DataAccess.Models;
using JobTrackerDataManager.DTOs;

namespace JobTrackerDataManager.Utilities;

public static class Utilities
{
    public static List<JobModel> MapKarriereAtDtosToJobs(List<KarriereAtDto> dtos)
    {
        List<JobModel> jobs = new();

        foreach (var dto in dtos)
        {
            JobModel job = new()
            {
                Title = dto.title,
                Location = dto.location,
                Company = dto.company,
                LinkToDetails = dto.title_link,
                Origin = "karriere.at",
                FirstSeenOn = DateTimeOffset.FromUnixTimeMilliseconds(dto.timestamp).DateTime,
                IsArchived = false
            };

            jobs.Add(job);
        }

        return jobs;
    }

    public static List<JobModel> MapDevJobsAtDtosToJobs(List<DevJobsDto> dtos)
    {
        List<JobModel> jobs = new();

        foreach (var dto in dtos)
        {
            // Ignore jobs that are not available anymore.
            if (dto.status.Contains("Inaktiv"))
            {
                continue;
            }

            // Enter Regex at https://regex101.com for explanation
            Regex regex = new Regex(@"((?<=\d\dk\s*)|^)[A-ZÄÖÜ][a-zäöüß][a-z.äöüß].*?((?=\s*\t)|$)");

            var newLocation = dto.location;

            var match = regex.Match(newLocation);

            JobModel job = new()
            {
                Title = dto.title,
                Location = match.ToString(),
                Company = dto.company,
                LinkToDetails = dto.description_link,
                Origin = "devjobs.at",
                FirstSeenOn = DateTimeOffset.FromUnixTimeMilliseconds(dto.timestamp).DateTime,
                IsArchived = false
            };

            jobs.Add(job);
        }

        return jobs;
    }

    public static List<JobModel> MapJobsAtDtosToJobs(List<JobsAtDto> dtos)
    {
        List<JobModel> jobs = new();

        foreach (var dto in dtos)
        {
            JobModel job = new()
            {
                Title = dto.title,
                Location = dto.location,
                Company = dto.company,
                LinkToDetails = dto.title_link,
                Origin = "jobs.at",
                FirstSeenOn = DateTimeOffset.FromUnixTimeMilliseconds(dto.timestamp).DateTime,
                IsArchived = false
            };

            jobs.Add(job);
        }

        return jobs;
    }
}