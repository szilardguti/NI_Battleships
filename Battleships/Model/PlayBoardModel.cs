using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Battleships.Model
{
    public class PlayBoardModel
    {
        public PlayBoardModel(Collection<Collection<TileStatus>> tiles)
        {
            this.Tiles = tiles;
        }

        public Collection<Collection<TileStatus>> Tiles { get; set; }

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
