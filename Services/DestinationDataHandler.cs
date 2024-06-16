using System.Threading.Tasks;

namespace SoccerStatisticApp.Services
{
    public class DestinationDataHandler
    {
        private const string path = "DataResources/DestinationData.txt";

        public static async Task WriteLineAsync(string jsonLine)
        {
            await File.AppendAllTextAsync(path, jsonLine + Environment.NewLine);
            await Task.Delay(1000);
        }
    }
}