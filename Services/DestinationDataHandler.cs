using System.Threading.Tasks;
using SoccerStatisticApp.Models;

namespace SoccerStatisticApp.Services
{
    /*  Class responsible for read and write operations 
        to the json data destination source. 
        */
    public class DestinationDataHandler
    {
        private const string path = @"DataResources\DestinationData.txt";


        public static async Task WriteLineAsync(string jsonLine)
        {
            await File.AppendAllTextAsync(path, jsonLine + Environment.NewLine);
            await Task.Delay(1000);
        }

        public static Statistic? ReadLineById(int id)
        {
            var lines = File.ReadAllLines(path);
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

        public void Dispose()
        {
            File.WriteAllText(path, string.Empty);
        }

    }
}
