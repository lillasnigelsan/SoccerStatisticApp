using System.Text.RegularExpressions;
using SoccerStatisticApp.Models;
using Microsoft.Extensions.Logging;

namespace SoccerStatisticApp.Services
{
    /*  Class responsible for recieveing new soccer statistics 
        data and convert it to Statistic objects.
        */
    public class SourceDataHandler
    {
        private const string path = @"DataResources\SourceData.txt";
        private static SourceDataHandler? _instance;
        private readonly IDestinationDataHandler _destinationDataHandler;
        private readonly ILogger<SourceDataHandler> _logger;

        private SourceDataHandler(DestinationDataHandler destinationDataHandler, ILogger<SourceDataHandler> logger)
        {
            _destinationDataHandler = destinationDataHandler;
            _logger = logger;

        }

        public static SourceDataHandler GetInstance(DestinationDataHandler destinationDataHandler, ILogger<SourceDataHandler> logger)
        {
            _instance ??= new SourceDataHandler(destinationDataHandler, logger);
            return _instance;
        }

        public async Task ReadSourceAsync()
        {
            string regexPattern = @"^\d+;([^;-]+)-(?!\1)([^;-]+);\d{4}-\d{2}-\d{2};\d+-\d+;\d+-\d+$";


            string[] lines = ReadLines(path);
            foreach (var line in lines)
            {
                if (!Regex.IsMatch(line, regexPattern))
                {
                    _logger.LogInformation($"Regex cought error in this line, and skipped to the next.");
                    continue;
                }


                Statistic statistic = CreateStatistic(line);

                if (await _destinationDataHandler.CheckIfExistsAsync(statistic.Id))
                {
                    _logger.LogInformation($"Id {statistic.Id} already exists, skipp line");
                    continue;
                }

                string json = statistic.ToJson();
                await _destinationDataHandler.WriteLineAsync(json);
            }
        }


        public static string[] ReadLines(string path)
        {
            return File.ReadAllLines(path);
        }

        public static Statistic CreateStatistic(string line)
        {
            string[] splitLine = line.Split(';');
            return new Statistic
            {
                Id = int.Parse(splitLine[0]),
                Description = splitLine[1],
                MatchStartTime = DateTime.Parse(splitLine[2]),
                Results =
                [
                    new() {Type = 0, Home = int.Parse(splitLine[3].Split('-')[0]), Away = int.Parse(splitLine[3].Split('-')[1])},
                    new() {Type = 1, Home = int.Parse(splitLine[4].Split('-')[0]), Away = int.Parse(splitLine[4].Split('-')[1])}
                ]
            };
        }

    }
}