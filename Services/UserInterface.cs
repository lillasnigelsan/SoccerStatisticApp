namespace SoccerStatisticApp.Services
{
    /*  Class responsible for interaction (send and recive text)
        with the console - which in this consol application 
        is the actual user interface.
        */
    public class UserInterface
    {
        public async Task<string> GetUserInputAsync()
        {
            return await Task.Run(() => GetUserInput());
        }

        public static void DisplayMessage(string message)
        {
            Console.WriteLine(message);
        }

        private static string GetUserInput()
        {
            return Console.ReadLine() ?? string.Empty; //Avoids null if user puts in multiple rows
        }
    }
}
