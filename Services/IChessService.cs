using ChessAnalyzer.Models;

namespace ChessAnalyzer.Services;

public interface IChessService
{
    Task<List<Game>> GetGamesAsync(string username);
}