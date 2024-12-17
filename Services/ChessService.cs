using Newtonsoft.Json;
using ChessAnalyzer.Models;

namespace ChessAnalyzer.Services;

public class ChessService(HttpClient httpClient) : IChessService
{
    public async Task<List<Game>> GetGamesAsync(string username)
    {
        string url = $"https://api.chess.com/pub/player/{username}/games/{DateTime.Today.Year}/{DateTime.Today.Month}";
        
        var response = await httpClient.GetStringAsync(url);
        return GetGameFromJsonData(username, response);
    }

    
    public async Task<List<Game>> GetGamesAsyncTest(string username)
    {
        var json = await File.ReadAllTextAsync(
            "C:\\Users\\mohar\\source\\repos\\ChessAnalyzer\\ChessAnalyzer\\JsonTests\\games.json");
        return GetGameFromJsonData("RedSeeds", json);
    }
    private static List<Game> GetGameFromJsonData(string username, string response)
    {
        var games = new List<Game>();
        var gamesResponse = JsonConvert.DeserializeObject<GamesResponse>(response);

        if(gamesResponse?.Games == null) return games;
        
        foreach (var gameData in gamesResponse.Games)
        {
            var opponent = gameData.White.Username == username ? gameData.Black.Username : gameData.White.Username;
            var result = gameData.White.Username == username ? gameData.White.Result : gameData.Black.Result;

            games.Add(new Game() { Player = username, Opponent = opponent, Result = result, PGN = gameData.PGN , UUID = gameData.UUID});
        }

        return games;
    }

}