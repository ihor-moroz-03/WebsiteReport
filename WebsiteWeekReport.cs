using System;
using System.Collections.Generic;
using System.Net;
using Algorithms;

namespace WebsiteReport
{
    class WebsiteWeekReport
    {
        Dictionary<IPAddress, List<Visit>> _records;

        public WebsiteWeekReport(string[] records)
        {
            _records = new Dictionary<IPAddress, List<Visit>>();
            try
            {
                foreach (string record in records)
                {
                    string[] temp = record.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    IPAddress ip = IPAddress.Parse(temp[0]);
                    Visit visit = new Visit
                    {
                        day = Enum.Parse<DayOfWeek>(char.ToUpper(temp[2][0]) + temp[2][1..]),
                        time = DateTime.Parse(temp[1])
                    };
                    if (_records.ContainsKey(ip)) _records[ip].Add(visit);
                    else _records[ip] = new List<Visit> { visit };
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Format seems to be wrong, check your file and try again");
            }
        }

        public string GetReport()
        {
            var report = new List<string>();
            var allRecords = new List<Visit>();
            foreach (var pair in _records)
            {
                report.Add(
                    $"ip: {pair.Key}:\n" +
                    $"{pair.Value.Count} visits\n" +
                    $"{Statistics.Mode(pair.Value.Select(visit => visit.day))} is most popular\n" +
                    $"{Statistics.Mode(pair.Value.Select(visit => visit.time.Hour))} o'clock is most popular\n"
                );
                allRecords.AddRange(pair.Value);
            }

            report.Add($"\n{Statistics.Mode(allRecords.Select(visit => visit.time.Hour))} o'clock is most popular among all IPs");

            return string.Join<string>('\n', report);
        }

        public override string ToString()
        {
            var res = new List<string>();
            foreach (var pair in _records)
            {
                foreach (Visit visit in pair.Value)
                {
                    res.Add($"{pair.Key} {visit.time.TimeOfDay} {visit.day}");
                }
            }
            return string.Join<string>('\n', res);
        }

        public struct Visit
        {
            public DayOfWeek day;
            public DateTime time;
        }
    }
}
