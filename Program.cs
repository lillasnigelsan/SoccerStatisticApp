using SoccerStatisticApp.Services;

/*  This is the program entrypiont, 
    responsible for initiating dependencies
    and starting upp the application. 
    */

DestinationDataHandler destinationDataHandler = new();
DestinationDataHandler destinationDataHandler2 = new();
SourceDataHandler sourceDataHandler = SourceDataHandler.GetInstance(destinationDataHandler);
UserInterface UI = new();
InputOutputProcessor inputOutputProcessor = new(UI, destinationDataHandler2);
App app = new(sourceDataHandler, inputOutputProcessor);

await app.RunAsync();