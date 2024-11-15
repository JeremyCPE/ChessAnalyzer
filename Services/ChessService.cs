﻿using System.Text.Json;
using ChessAnalyzer.Models;

namespace ChessAnalyzer.Services;

public class ChessService(HttpClient httpClient) : IChessService
{
    public async Task<List<Game>> GetGamesAsync(string username)
    {
        var games = new List<Game>();

        
        string url = $"https://api.chess.com/pub/player/{username}/games/{DateTime.Today.Year}/{DateTime.Today.Month}";
        
        var response = await httpClient.GetStringAsync(url);
        var archives = JsonSerializer.Deserialize<ArchiveResponse>(response);

        if (archives?.Archives?.Length > 0)
        {
            string latestArchiveUrl = archives.Archives[^1];
            var gamesResponse = await httpClient.GetStringAsync(latestArchiveUrl);
            var gamesData = JsonSerializer.Deserialize<GamesResponse>(gamesResponse);

            foreach (var gameData in gamesData?.Games ?? Array.Empty<GameResponse>())
            {
                var opponent = gameData.White.Username == username ? gameData.Black.Username : gameData.White.Username;
                var result = gameData.White.Username == username ? gameData.White.Result : gameData.Black.Result;
                
                games.Add(new Game() { Opponent = opponent, Result = result });
            }
        }

        return games;
    }
}