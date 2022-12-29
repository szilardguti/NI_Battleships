using System.Collections.Generic;
using System.Linq;
using Battleships.DAL.Entities;

namespace Battleships.DAL
{
    public class ResultRepository : IResultRepository
    {
        public GameResult CreateGameResult(GameResult gameResult)
        {
            using var context = new GameResultContext();

            context.Results.Add(gameResult);
            context.SaveChanges();

            return gameResult;
        }

        public void UpdateGameResult(GameResult gameResult)
        {
            using var context = new GameResultContext();

            context.Results.Update(gameResult);
            context.SaveChanges();
        }

        public ICollection<GameResult> GetAllGames()
        {
            using var context = new GameResultContext();

            return context.Results.ToList();
        }

        public void AddActionToGame(GameAction gameAction)
        {
            using var context = new GameResultContext();

            context.Actions.Add(gameAction);
            context.SaveChanges();
        }

        public ICollection<GameAction> GetAllActions(int gameResultId)
        {
            using var context = new GameResultContext();

            return context.Actions.Where(act => act.GameResult.Id == gameResultId).ToList();
        }
    }
}
