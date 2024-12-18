namespace ChessAnalyzer.Models;

public class ChessSquare
{
    public string Row { get; set; }
    public string Column { get; set; }
    public Color Color { get; set; }
    public string PieceImage { get; set; }

    public string Position => $"{Row}/{Column}";
}