namespace ChessAnalyzer.Models;

/// <summary>
/// Game that we gonna use for the analyse
/// </summary>
public class Game
{
    public required string Player { get; set; }
    public required string Opponent { get; set; }
    public required string Result { get; set; }

    public required string UUID { get; set; }

    public required string PGN { get; set; }

    public required string FEN { get; set; }
}