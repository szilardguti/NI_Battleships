using Battleships.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Battleships.Model
{
    public class PlayBoardModel
    {
        public Collection<Tile> Tiles { get; set; }

        public Collection<ShipModel> Ships { get; set; }

        public PlayBoardModel(Collection<Tile> tiles, Collection<ShipModel> ships)
        {
            Tiles = tiles;
            Ships = ships;
        }

        public Tile GetTile(int rowIndex, int colIndex)
        {
            return Tiles.FirstOrDefault(tile => tile.X == rowIndex && tile.Y == colIndex);
        }
        public void SetTile(int rowIndex, int colIndex, TileStatus tileStatus)
        {
            Tiles.FirstOrDefault(tile => tile.X == rowIndex && tile.Y == colIndex).TileStatus = tileStatus;
        }
    }
}
