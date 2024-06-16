using SoccerStatisticApp.Models;

namespace SoccerStatisticApp.Services
{
    /*  Class responsible for recieveing new soccer statistics 
        data and convert it to Statistic objects.
        */
    public class SourceDataHandler
    {
        private const string path = @"DataResources\SourceData.txt";
        private static SourceDataHandler? _instance;
        private readonly DestinationDataHandler _destinationDataHandler;

        private SourceDataHandler(DestinationDataHandler destinationDataHandler)
        {
            _destinationDataHandler = destinationDataHandler;
        }

        public static SourceDataHandler GetInstance(DestinationDataHandler destinationDataHandler)
        {
            _instance ??= new SourceDataHandler(destinationDataHandler);
            return _instance;
        }

        public static async Task ReadSourceAsync()
        {
            string[] lines = ReadLines(path);
            foreach (var line in lines)
            {
                Statistic statistic = CreateStatistic(line);
                string json = statistic.ToJson();
                await DestinationDataHandler.WriteLineAsync(json);
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