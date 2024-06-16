using System;
using System.Threading.Tasks;
using SoccerStatisticApp.Models;
using SoccerStatisticApp.Services;

namespace SoccerStatisticApp.Services
{
    /*  Class responsible for interaction (send and recive text)
        with the console - which in this consol application 
        is the actual user interface.
        */
    public class InputOutputProcessor(UserInterface UI, DestinationDataHandler destinationDataHandler)
    {
        private readonly UserInterface _UI = UI;
        private readonly DestinationDataHandler _destinationDataHandler = destinationDataHandler;

        public async Task UserInterfaceAsync()
        {
            while (true)
            {
                UserInterface.DisplayMessage("Please enter a command (Q to quit, 1-10 to fetch statistic):");
                string input = (await _UI.GetUserInputAsync()).ToLower();
                ProcessInput(input);
            }
        }

        private void ProcessInput(string input)
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
            Statistic? stat = DestinationDataHandler.ReadLineById(id);

            if (stat == null)
            {
                UserInterface.DisplayMessage("Hittade ingen match med detta id.");
                return;
            }

            string formatedMessage = StatisticToString(stat);
            UserInterface.DisplayMessage(formatedMessage);
        }

        //TODO: Check number of entries in destinationdata..
        private static bool ValidateInput(string input)
        {
            return input == "q" || (int.TryParse(input, out int number) && number >= 1 && number <= 10);
        }

        private static string StatisticToString(Statistic data)
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
                return $"{gameTeamsDate} \n - {resultMidGame} \n - {resultTotal}";
            }
            catch (Exception)
            {
                return "Objektet innehåller inte komplett matchdata.";
            }
        }

        private void CloseDown()
        {
            _destinationDataHandler.Dispose();
            Environment.Exit(0);
        }
    }
}
