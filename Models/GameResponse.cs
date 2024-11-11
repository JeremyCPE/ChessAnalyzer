namespace ChessAnalyzer.Models;

public class GameResponse
{
    public Player White{get;set;}
    public Player Black {get;set;}
    
    public string PGN {get;set;}
    public string FEN {get;set;}
     
    public TimeSpan StartTime {get;set;}
    public TimeSpan EndTime { get; set; }
}