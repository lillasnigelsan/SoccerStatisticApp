using SoccerStatisticApp.Models;

namespace SoccerStatisticApp.Services
{
    public class SourceDataHandler
    {
        private const string path = "DataResources/SourceData.txt";
        private static SourceDataHandler? _instance;
        readonly DestinationDataHandler destinationDataHandler = new();

        public SourceDataHandler(DestinationDataHandler destinationDataHandler)
        {
            this.destinationDataHandler = destinationDataHandler;
        }

        private SourceDataHandler() { }

        public static SourceDataHandler GetInstance()
        {
            _instance ??= new SourceDataHandler();
            return _instance;
        }



        public async Task ReadSourceAsync()
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

        public Statistic CreateStatistic(string line)
        {
            string[] splitLine = line.Split(';');
            return new Statistic
            {
                Id = int.Parse(splitLine[0]),
                Description = splitLine[1],
                MatchStartTime = DateTime.Parse(splitLine[2]),
                Results = new List<Results>
                {
                    new Results {type = 0, home = int.Parse(splitLine[3].Split('-')[0]), away = int.Parse(splitLine[3].Split('-')[1])},
                    new Results {type = 1, home = int.Parse(splitLine[4].Split('-')[0]), away = int.Parse(splitLine[4].Split('-')[1])}
                }
            };
        }

    }
}