using Microsoft.Extensions.Logging;

namespace SoccerStatisticApp.Services
{
    /*  Class responcible for starting up all asynncronous workflows.
        */
    public class App(SourceDataHandler sourceDataHandler, InputOutputProcessor inputOutputProcessor, ILogger<App> logger)
    {

        private readonly SourceDataHandler _sourceDataHandler = sourceDataHandler;
        private readonly InputOutputProcessor _inputOutputProcessor = inputOutputProcessor;
        private readonly ILogger<App> _logger = logger;

        public async Task RunAsync()
        {
            _logger.LogInformation("Starting upp asyncronous flows.");
            Task readSourceAsync = _sourceDataHandler.ReadSourceAsync();
            Task userInterfaceAsync = _inputOutputProcessor.UserInterfaceAsync();

            await Task.WhenAll(readSourceAsync, userInterfaceAsync);
        }
    }
}