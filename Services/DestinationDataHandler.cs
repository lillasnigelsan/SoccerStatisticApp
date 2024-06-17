using SoccerStatisticApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SoccerStatisticApp.Services
{
    public class DestinationDataHandler : IDestinationDataHandler
    {
        private const string path = @"DataResources\DestinationData.txt";

        public async Task WriteLineAsync(string jsonLine)
        {
            await File.AppendAllTextAsync(path, jsonLine + Environment.NewLine);
            await Task.Delay(1000);
        }

        public async Task<Statistic?> ReadLineByIdAsync(int id)
        {
            var lines = await File.ReadAllLinesAsync(path);
            foreach (var line in lines)
            {
                Statistic stat = Statistic.FromJson(line);
                if (stat.Id == id)
                {
                    return stat;
                }
            }
            return null;
        }

        public async Task<bool> CheckIfExistsAsync(int id)
        {
            var lines = await File.ReadAllLinesAsync(path);
            bool exists = false;

            foreach (var line in lines)
            {
                Statistic stat = Statistic.FromJson(line);
                if (stat.Id == id)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }


        public async Task<List<string>> GetEntryInfoAsync()
        {
            List<string> ranges = new List<string>();
            List<int> idList = new List<int>();

            var lines = await File.ReadAllLinesAsync(path);
            foreach (var line in lines)
            {
                Statistic stat = Statistic.FromJson(line);
                idList.Add(stat.Id);
            }

            if (idList.Count == 0)
            {
                return ranges;
            }

            idList.Sort();

            int start = idList[0];

            for (int i = 1; i < idList.Count; i++)
            {
                if (idList[i] != idList[i - 1] + 1)
                {
                    ranges.Add($"{start}-{idList[i - 1]}");
                    start = idList[i];
                }
            }

            ranges.Add($"{start}-{idList[^1]}");

            return ranges;
        }

        public void Dispose()
        {
            File.WriteAllText(path, string.Empty);
        }
    }
}