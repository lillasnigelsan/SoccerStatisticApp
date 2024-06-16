using SoccerStatisticApp.Services;

SourceDataHandler sourceDataHandler = SourceDataHandler.GetInstance();
App app = new(sourceDataHandler);

await app.RunAsync();