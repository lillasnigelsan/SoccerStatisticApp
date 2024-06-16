namespace SoccerStatisticApp.Services
{
    public class App(SourceDataHandler sourceDataHandler)
    {
        private SourceDataHandler _sourceDataHandler = sourceDataHandler;

        public async Task RunAsync()
        {
            Task readSourceAsync = _sourceDataHandler.ReadSourceAsync();
            //TODO: Task userInterfaceAsync = UserInterfaceAsync();

            await Task.WhenAll(readSourceAsync);
            //TODO: await Task.WhenAll(readSourceAsync, userInterfaceAsync);
        }
    }
}