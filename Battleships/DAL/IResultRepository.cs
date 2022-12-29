using System;
using System.Collections.Generic;
using System.Text;
using Battleships.DAL.Entities;

namespace Battleships.DAL
{
    public interface IResultRepository
    {
        public GameResult CreateGameResult(GameResult gameResult);

        public void UpdateGameResult(GameResult gameResult);

        public ICollection<GameResult> GetAllGames();

        public void AddActionToGame(GameAction gameAction);

        public ICollection<GameAction> GetAllActions(int gameResultId);
    }
}
