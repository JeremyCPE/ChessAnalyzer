public class ChessGame
{
    public string CurrentPosition { get; set; }
    public List<Move> Moves { get; set; } = new List<Move>();
}

public class Move
{
    public string Notation { get; set; }
    public TimeSpan Clock { get; set; }
}
