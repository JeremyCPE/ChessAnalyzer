using ChessAnalyzer.Models;

namespace ChessAnalyzer.Controller;

public class ChessComController
{
   public GameResponse SearchByGameId(string gameId)
   {
      return new GameResponse();
   }
   
   public List<GameResponse> SearchByPseudo(string pseudo)
   {
      return new List<GameResponse>();
   }
}