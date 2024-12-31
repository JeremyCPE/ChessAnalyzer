using System.Text.RegularExpressions;

namespace ChessAnalyser.Service
{
    public class ChessParser
    {
        public static ChessGame ParsePgn(string pgnData)
        {
            List<ChessMove> moves = new();

            string pattern = @"(\d+)\.\s(\w+)\s\{%\[clk\s([\d:.]+)\]\}|(\.\.\.\s(\w+)\s\{%\[clk\s([\d:.]+)\]\})";

            // Regex for White's move
            string whiteMovePattern = @"(\d+)\.\s(\w+)\s\{%\[clk\s([\d:.]+)\]\}";
            // Regex for Black's move
            string blackMovePattern = @"\.\.\.\s(\w+)\s\{%\[clk\s([\d:.]+)\]\}";

            MatchCollection whiteMatches = Regex.Matches(pgnData, whiteMovePattern);
            MatchCollection blackMatches = Regex.Matches(pgnData, blackMovePattern);



            MatchCollection matches = Regex.Matches(pgnData, pattern);

            foreach (Match match in matches)
            {
                int moveNumber = int.Parse(match.Groups[1].Value);

                // White move
                moves.Add(new ChessMove
                {
                    MoveNumber = moveNumber,
                    Player = "White",
                    Move = match.Groups[2].Value,
                    Clock = match.Groups[3].Value
                });

                // Black move (if exists)
                if (match.Groups[5].Success)
                {
                    moves.Add(new ChessMove
                    {
                        MoveNumber = moveNumber,
                        Player = "Black",
                        Move = match.Groups[5].Value,
                        Clock = match.Groups[6].Value
                    });
                }
            }
            ChessGame game = new()
            {
                Moves = moves
            };
            return game;

        }

    }
}

