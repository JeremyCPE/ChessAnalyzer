using ChessAnalyzer.Models;

namespace ChessAnalyzer.Controller;

public class ChessComController
{
   public Game SearchByGameId(string gameId)
   {
      return new Game();
   }
   
   public List<Game> SearchByPseudo(string pseudo)
   {
      return new List<Game>();
   }
}