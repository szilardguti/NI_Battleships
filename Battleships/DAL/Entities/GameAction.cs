using System.Collections.Generic;
using Battleships.Model;
using Battleships.Model.Helpers;

namespace Battleships.DAL.Entities
{
    public class GameAction
    {
        public int Id { get; set; }

        public string PlayerName { get; set; }

        public string ActionString { get; set; }

        public GameResult GameResult { get; set; }

        public ICollection<Tile> FirstPlayerBoardStatus { get; set; }

        public ICollection<Tile> SecondPlayerBoardStatus { get; set; }

        public ICollection<ShipModel> FirstPlayerShips { get; set; }

        public ICollection<ShipModel> SecondPlayerShips { get; set; }
    }
}