using Microsoft.Extensions.Logging;
using SoccerStatisticApp.Services;

/*  This is the program entrypiont, 
    responsible for initiating dependencies
    and starting upp the application. 
    */

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
{
    builder.AddConsole();
});
ILogger<Program> logger = loggerFactory.CreateLogger<Program>();
DestinationDataHandler destinationDataHandler = new();
SourceDataHandler sourceDataHandler = SourceDataHandler.GetInstance(destinationDataHandler, loggerFactory.CreateLogger<SourceDataHandler>());
UserInterface UI = new();
InputOutputProcessor inputOutputProcessor = new(UI, destinationDataHandler, loggerFactory.CreateLogger<InputOutputProcessor>());
App app = new(sourceDataHandler, inputOutputProcessor, loggerFactory.CreateLogger<App>());

logger.LogInformation("Starting up application.");
await app.RunAsync();