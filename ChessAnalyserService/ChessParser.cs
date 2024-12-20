using System.Globalization;
using System.Text.RegularExpressions;

namespace ChessAnalyser.Service
{
    public class ChessParser
    {
        public static ChessGame ParsePgn(string pgnData)
        {
            ChessGame game = new();

            // Extract the CurrentPosition
            Match currentPositionMatch = Regex.Match(pgnData, @"\[CurrentPosition\s+""([^""]+)""\]");
            if (currentPositionMatch.Success)
            {
                game.CurrentPosition = currentPositionMatch.Groups[1].Value;
            }

            // Extract moves and clock times
            string movesSection = Regex.Match(pgnData, @"1\..+").Value; // Find the moves section starting from "1."
            MatchCollection moveMatches = Regex.Matches(movesSection, @"(?<move>[a-h1-8=+#]{2,}|\.\.\.)\s*\{%\[clk\s+(?<time>[0-9:.]+)\]\}");

            foreach (Match match in moveMatches)
            {
                if (match.Success)
                {
                    Move move = new()
                    {
                        Notation = match.Groups["move"].Value,
                        Clock = TimeSpan.ParseExact(match.Groups["time"].Value, "m\\:ss\\.f", CultureInfo.InvariantCulture)
                    };
                    game.Moves.Add(move);
                }
            }

            return game;
        }
    }

}
