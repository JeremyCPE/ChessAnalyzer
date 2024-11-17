using System.Text.Json.Serialization;

namespace ChessAnalyzer.Models;
public class GamesResponse
{
    public GameResponse[] Games { get; set; }
}