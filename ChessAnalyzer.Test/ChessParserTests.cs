using ChessAnalyser.Service;

namespace ChessAnalyser.Test;
public class ChessParserTests
{
    [Fact]
    public void ParsePgn_ShouldExtractCurrentPosition()
    {
        // Arrange
        string pgnData = @"
        [Event ""Live Chess""]
        [Site ""Chess.com""]
        [Date ""2024.11.11""]
        [Round ""-""]
        [White ""RedSeeds""]
        [Black ""mustafa2266""]
        [Result ""0-1""]
        [CurrentPosition ""r1b5/ppR4R/4pkp1/4n3/4p1r1/8/PPP3P1/2K5 w - -""]

        1. e4 {[%clk 0:10:00]} 1... g6 {[%clk 0:09:57.4]} 
        2. d4 {[%clk 0:09:55.6]} 2... Bg7 {[%clk 0:09:56.3]} 
        3. Nf3 {[%clk 0:09:54.9]} 3... e6 {[%clk 0:09:55.7]} 4. h4 {[%clk 0:09:48.8]}
        ";

        // Act
        ChessGame game = ChessParser.ParsePgn(pgnData);

        // Assert
        Assert.NotNull(game);
        Assert.Equal("r1b5/ppR4R/4pkp1/4n3/4p1r1/8/PPP3P1/2K5 w - -", game.CurrentPosition);
    }

    [Fact]
    public void ParsePgn_ShouldExtractMovesWithClockTimes()
    {
        // Arrange
        string pgnData = @"
        [Event ""Live Chess""]
        [Site ""Chess.com""]
        [Date ""2024.11.11""]
        [Round ""-""]
        [White ""RedSeeds""]
        [Black ""mustafa2266""]
        [Result ""0-1""]
        [CurrentPosition ""r1b5/ppR4R/4pkp1/4n3/4p1r1/8/PPP3P1/2K5 w - -""]

        1. e4 {[%clk 0:10:00]} 1... g6 {[%clk 0:09:57.4]} 
        2. d4 {[%clk 0:09:55.6]} 2... Bg7 {[%clk 0:09:56.3]} 
        3. Nf3 {[%clk 0:09:54.9]} 3... e6 {[%clk 0:09:55.7]} 4. h4 {[%clk 0:09:48.8]}
        ";

        // Act
        ChessGame game = ChessParser.ParsePgn(pgnData);

        // Assert
        Assert.NotNull(game);
        Assert.Equal(7, game.Moves.Count);

        // Validate the first move
        Assert.Equal("e4", game.Moves[0].Notation);
        Assert.Equal(TimeSpan.FromMinutes(10), game.Moves[0].Clock);

        // Validate the second move
        Assert.Equal("g6", game.Moves[1].Notation);
        Assert.Equal(new TimeSpan(0, 0, 9, 57, 400), game.Moves[1].Clock);

        // Validate the last move
        Assert.Equal("h4", game.Moves.Last().Notation);
        Assert.Equal(new TimeSpan(0, 0, 9, 48, 800), game.Moves.Last().Clock);
    }

    [Fact]
    public void ParsePgn_ShouldHandleEmptyInputGracefully()
    {
        // Arrange
        string emptyPgn = "";

        // Act
        ChessGame game = ChessParser.ParsePgn(emptyPgn);

        // Assert
        Assert.NotNull(game);
        Assert.Null(game.CurrentPosition);
        Assert.Empty(game.Moves);
    }

    [Fact]
    public void ParsePgn_ShouldHandleMissingClockTimes()
    {
        // Arrange
        string pgnWithoutClocks = @"
        [Event ""Live Chess""]
        [Site ""Chess.com""]
        [Date ""2024.11.11""]
        [Round ""-""]
        [White ""RedSeeds""]
        [Black ""mustafa2266""]
        [Result ""0-1""]
        [CurrentPosition ""r1b5/ppR4R/4pkp1/4n3/4p1r1/8/PPP3P1/2K5 w - -""]

        1. e4 1... g6 
        2. d4 2... Bg7 
        3. Nf3 3... e6 4. h4
        ";

        // Act
        ChessGame game = ChessParser.ParsePgn(pgnWithoutClocks);

        // Assert
        Assert.NotNull(game);
        Assert.Equal(8, game.Moves.Count);

        // Validate moves without clock times
        Assert.All(game.Moves, move => Assert.Equal(TimeSpan.Zero, move.Clock));
    }
}
