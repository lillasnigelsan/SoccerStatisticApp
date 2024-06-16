using System.Text.Json;

namespace SoccerStatisticApp.Models
{
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
        public int type { get; set; }
        public int home { get; set; }
        public int away { get; set; }
    }
}
