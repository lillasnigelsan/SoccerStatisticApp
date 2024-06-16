namespace SoccerStatisticApp.Services
{
    /*  Class responcible for starting up all asynncronous workflows.
        */
    public class App(SourceDataHandler sourceDataHandler, InputOutputProcessor inputOutputProcessor)
    {
        private readonly SourceDataHandler _sourceDataHandler = sourceDataHandler;
        private readonly InputOutputProcessor _inputOutputProcessor = inputOutputProcessor;

        public async Task RunAsync()
        {
            Task readSourceAsync = SourceDataHandler.ReadSourceAsync();
            Task userInterfaceAsync = _inputOutputProcessor.UserInterfaceAsync();

            await Task.WhenAll(readSourceAsync, userInterfaceAsync);
        }
    }
}