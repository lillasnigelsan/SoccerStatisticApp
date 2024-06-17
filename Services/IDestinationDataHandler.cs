using SoccerStatisticApp.Models;

namespace SoccerStatisticApp.Services
{
    public interface IDestinationDataHandler
    {
        Task WriteLineAsync(string jsonLine);
        Task<Statistic?> ReadLineByIdAsync(int id);
        Task<bool> CheckIfExistsAsync(int id);
        Task<List<string>> GetEntryInfoAsync();
        void Dispose();
    }
}
