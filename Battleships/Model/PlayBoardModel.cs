using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Battleships.Model
{
    public class PlayBoardModel
    {
        public Collection<Collection<TileStatus>> Tiles { get; set; }

        public Collection<ShipModel> Ships { get; set; }

        public PlayBoardModel(Collection<Collection<TileStatus>> tiles, Collection<ShipModel> ships)
        {
            Tiles = tiles;
            Ships = ships;
        }

        public TileStatus GetTile(int rowIndex, int colIndex)
        {
            return Tiles[rowIndex][colIndex];
        }
        public void SetTile(int rowIndex, int colIndex, TileStatus tileStatus)
        {
            Tiles[rowIndex][colIndex] = tileStatus;
        }
    }
}
