using System.Text.Json;

namespace SoccerStatisticApp.Models
{
    /*  Class representing a model used to create Statistic objects for each specific game, 
        can easily convert incoming (ToJson) and outgoing (FromJson) match data.
        */
    public class Statistic
    {
        public int Id { get; set; }
        public required string Description { get; set; }
        public DateTime MatchStartTime { get; set; }
        public required List<Results> Results { get; set; }

        public string ToJson()
        {
            return JsonSerializer.Serialize(this);
        }

        public static Statistic FromJson(string json)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return JsonSerializer.Deserialize<Statistic>(json);
#pragma warning restore CS8603 // Possible null reference return.
        }
    }

    public class Results
    {
        public int Type { get; set; }
        public int Home { get; set; }
        public int Away { get; set; }
    }
}
