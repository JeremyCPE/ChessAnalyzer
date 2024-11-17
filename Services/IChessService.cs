using ChessAnalyzer.Models;

namespace ChessAnalyzer.Services;

public interface IChessService
{
    Task<List<Game>> GetGamesAsync(string username);
    Task<List<Game>> GetGamesAsyncTest(string username);
}