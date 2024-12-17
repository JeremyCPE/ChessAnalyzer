namespace ChessAnalyzer.Models;

public class ChessSquare
{
    public int Row { get; set; }
    public int Column { get; set; }
    public Color Color { get; set; }
    public string PieceImage { get; set; }
}