using Newtonsoft.Json;

namespace ChessAnalyzer.Models;

public class GameResponse
{
    public Player White { get; set; }
    public Player Black { get; set; }

    public string PGN { get; set; }
    public string FEN { get; set; }

    public string UUID { get; set; }

    [JsonProperty("start_time")]
    public int StartTime { get; set; }
    [JsonProperty("end_time")]
    public int EndTime { get; set; }
    [JsonProperty("time_class")]
    public string TimeClass { get; set; }
    [JsonProperty("time_control")]
    public string TimeControl { get; set; }

    public Game ToGame(string username, string opponent, string result)
    {
        return new Game()
        {
            Player = username,
            Opponent = opponent,
            Result = result,
            PGN = this.PGN,
            UUID = this.UUID,
            FEN = this.FEN,
        };
    }
}