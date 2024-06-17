using SoccerStatisticApp.Models;
using Microsoft.Extensions.Logging;

namespace SoccerStatisticApp.Services
{
    /*  Class responsible for interaction (send and recive text)
        with the console - which in this consol application 
        is the actual user interface.
        */
    public class InputOutputProcessor(UserInterface UI, IDestinationDataHandler destinationDataHandler, ILogger<InputOutputProcessor> logger)
    {
        private readonly UserInterface _UI = UI;
        private readonly IDestinationDataHandler _destinationDataHandler = destinationDataHandler;
        private readonly ILogger<InputOutputProcessor> _logger = logger;

        public async Task UserInterfaceAsync()
        {
            while (true)
            {
                UserInterface.DisplayMessage("\nAlternativ:\n - Hämta statistik för en match genom att ange ett id.\n - Avsluta programmet med Q.");
                string input = (await _UI.GetUserInputAsync()).ToLower();
                await ProcessInput(input);
            }
        }

        private async Task ProcessInput(string input)
        {
            if (!ValidateInput(input))
            {
                UserInterface.DisplayMessage("Felaktig input. Försök igen!");
                return;
            }

            if (input == "q")
            {
                UserInterface.DisplayMessage("Du har valt att avsluta. Välkommen åter!");
                CloseDown();
            }

            int id = int.Parse(input);
            Statistic? stat = await _destinationDataHandler.ReadLineByIdAsync(id);

            if (stat == null)
            {
                UserInterface.DisplayMessage("\nHittade ingen match med detta id.");

                List<string> ranges = await _destinationDataHandler.GetEntryInfoAsync();
                if (ranges != null)
                {
                    UserInterface.DisplayMessage("Tillgängliga id:");
                    foreach (string range in ranges)
                    {
                        UserInterface.DisplayMessage(range);
                    }
                }
                return;
            }

            string formatedMessage = StatisticToStringMessage(stat);
            UserInterface.DisplayMessage(formatedMessage);
        }

        private static bool ValidateInput(string input)
        {
            return input == "q" || (int.TryParse(input, out int number) && number >= 1);
        }

        private static string StatisticToStringMessage(Statistic data)
        {
            try
            {
                Results? totalScores = data.Results.Find(r => r.Type == 0);
                Results? midGameScores = data.Results.Find(r => r.Type == 1);

                if (totalScores == null || midGameScores == null)
                {
                    throw new Exception("Incomplete match data");
                }

                string gameTeamsDate = $"Matchen med id {data.Id}, spelades mellan lagen {data.Description}, {data.MatchStartTime}.";
                string resultMidGame = $"Efter halvtid var resultatet {midGameScores.Home} - {midGameScores.Away}.";
                string resultTotal = $"Matchen slutade med {totalScores.Home} - {totalScores.Away}.";
                return $"\n{gameTeamsDate}\n - {resultMidGame}\n - {resultTotal}";
            }
            catch (Exception)
            {
                return "Objektet innehåller inte komplett matchdata.";
            }
        }

        private void CloseDown()
        {
            _logger.LogInformation("The application is closing down.");
            _destinationDataHandler.Dispose();
            Environment.Exit(0);
        }
    }
}
