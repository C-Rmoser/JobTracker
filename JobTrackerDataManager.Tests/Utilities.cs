using System;
using System.Collections.Generic;
using DataAccess.Models;
using JobTrackerDataManager.DTOs;

namespace JobTrackerDataManager.Tests;

public static class Utilities
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
                Company = "AXESS",
                LinkToDetails = "www.karriere.at/csharpjob",
                Origin = "Karriere.at",
                FirstSeenOn = DateTime.Now
            },
            new JobModel()
            {
                Id = 2,
                Title = "Javascript Developer",
                Location = "Wals",
                Company = "Eurofunk Kappacher",
                LinkToDetails = "www.jobs.at/jsjob",
                Origin = "Jobs.at",
                FirstSeenOn = DateTime.Now
            },
            new JobModel()
            {
                Id = 3,
                Title = "FullStack Developer",
                Company = "Windhager Zentralheizung",
                Location = "Haag",
                LinkToDetails = "www.dev-jobs.at/fullstackjob",
                Origin = "dev-jobs.at",
                FirstSeenOn = DateTime.Now
            }
        };

        return jobs;
    }

    public static List<KarriereAtDto> GetKarriereAtDtos()
    {
        List<KarriereAtDto> dtos = new()
        {
            new KarriereAtDto()
            {
                company = "eurofunk Kappacher GmbH",
                company_link = "https://www.karriere.at/f/eurofunk-kappacher",
                description = "FULLSTACK C# DEVELOPER (m/w/d) YOU ENJOY CODING WITH C#!",
                location = "Salzburg (Stadt), Sankt Johann im Pongau, Linz, Klagenfurt am Wörthe",
                location_link = "https://www.karriere.at/jobs/salzburg-stadt",
                title = "Fullstack C# Developer (m/w/d)",
                title_link = "https://www.karriere.at/jobs/6360305",
                index = 1,
                timestamp = 1660571415703,
                timestampstring = "Mon, 15 Aug 2022 13:50:15 GMT",
                uid = "uLnMJ2gjKi8q2NaoEUJf",
                url = "https://www.karriere.at/jobs/c%23/salzburg-stadt?radius=30",
                url_uid = "1"
            },
            new KarriereAtDto()
            {
                company = "siconnex customized solutions GmbH	",
                company_link = "https://www.karriere.at/f/eurofunk-kappacher",
                description = "Ihre Aufgaben: Entwicklungen unserer Maschinensoftware in C# unt",
                location = "Salzburg (Stadt), Sankt Johann im Pongau, Linz, Klagenfurt am Wörthe",
                location_link = "https://www.karriere.at/jobs/salzburg-stadt",
                title = "Fullstack C# Developer (m/w/d)",
                title_link = "https://www.karriere.at/jobs/6360305",
                index = 1,
                timestamp = 1660571415703,
                timestampstring = "Mon, 15 Aug 2022 13:50:15 GMT",
                uid = "uLnMJ2gjKi8q2NaoEUJf",
                url = "https://www.karriere.at/jobs/c%23/salzburg-stadt?radius=30",
                url_uid = "1"
            },
            new KarriereAtDto()
            {
                company = "FERCHAU Austria GmbH",
                company_link = "https://www.karriere.at/f/eurofunk-kappacher",
                description = "Dein Aufgabengebiet: Entwicklung und Implementierung von projektsp",
                location = "Salzburg (Stadt), Sankt Johann im Pongau, Linz, Klagenfurt am Wörthe",
                location_link = "https://www.karriere.at/jobs/salzburg-stadt",
                title = "Fullstack C# Developer (m/w/d)",
                title_link = "https://www.karriere.at/jobs/6360305",
                index = 1,
                timestamp = 1660571415703,
                timestampstring = "Mon, 15 Aug 2022 13:50:15 GMT",
                uid = "uLnMJ2gjKi8q2NaoEUJf",
                url = "https://www.karriere.at/jobs/c%23/salzburg-stadt?radius=30",
                url_uid = "1"
            }
        };

        return dtos;
    }

    public static List<DevJobsDto> GetDevJobsAtDtos()
    {
        List<DevJobsDto> dtos = new()
        {
            new DevJobsDto()
            {
                company = "AXESS AG",
                company_link = "https://www.karriere.at/f/eurofunk-kappacher",
                description = "Entwickeln von Web Applikationen im Frontend und Backend Bereich",
                description_link = "",
                location = "Salzburg (Stadt), Sankt Johann im Pongau, Linz, Klagenfurt am Wörthe",
                location_link = "https://www.karriere.at/jobs/salzburg-stadt",
                status = "",
                title = "Fullstack C# Developer (m/w/d)",
                index = 1,
                timestamp = 1660571415703,
                timestampstring = "Mon, 15 Aug 2022 13:50:15 GMT",
                uid = "uLnMJ2gjKi8q2NaoEUJf",
                url = "https://www.karriere.at/jobs/c%23/salzburg-stadt?radius=30",
                url_uid = "1"
            },
            new DevJobsDto()
            {
                company = "advastore GmbH",
                company_link = "https://www.karriere.at/f/eurofunk-kappacher",
                description = "Disziplinarische Fhrung und Koordination des Teams",
                description_link = "",
                location = "Salzburg (Stadt), Sankt Johann im Pongau, Linz, Klagenfurt am Wörthe",
                location_link = "https://www.karriere.at/jobs/salzburg-stadt",
                status = "",
                title = "Fullstack C# Developer (m/w/d)",
                index = 1,
                timestamp = 1660571415703,
                timestampstring = "Mon, 15 Aug 2022 13:50:15 GMT",
                uid = "uLnMJ2gjKi8q2NaoEUJf",
                url = "https://www.karriere.at/jobs/c%23/salzburg-stadt?radius=30",
                url_uid = "1"
            },
            new DevJobsDto()
            {
                company = "Atos Solution & Services GmbH	",
                company_link = "https://www.karriere.at/f/eurofunk-kappacher",
                description = "Entwicklung und Anpassungen von Web Applikationen auf Basis Mic",
                description_link = "",
                location = "Salzburg (Stadt), Sankt Johann im Pongau, Linz, Klagenfurt am Wörthe",
                location_link = "https://www.karriere.at/jobs/salzburg-stadt",
                status = "",
                title = "Fullstack C# Developer (m/w/d)",
                index = 1,
                timestamp = 1660571415703,
                timestampstring = "Mon, 15 Aug 2022 13:50:15 GMT",
                uid = "uLnMJ2gjKi8q2NaoEUJf",
                url = "https://www.karriere.at/jobs/c%23/salzburg-stadt?radius=30",
                url_uid = "1"
            }
        };

        return dtos;
    }
}