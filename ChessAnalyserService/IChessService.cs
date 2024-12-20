using ChessAnalyzer.Models;

namespace ChessAnalyser.Service;

public interface IChessService
{
    Task<List<Game>> GetGamesAsync(string username);
    Task<List<Game>> GetGamesAsyncTest(string username);
}