public class ChessGame
{
    public string CurrentPosition { get; set; }
    public List<ChessMove> Moves { get; set; } = new List<ChessMove>();
}

public class ChessMove
{
    public int MoveNumber { get; set; }
    public string Player { get; set; }
    public string Notation { get; set; }
    public string Move { get; set; }

    public string Clock { get; set; }
}
