using Battleships.Model.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Battleships.Model
{
    public class ShipModel
    {
        public string Name { get; set; }

        public int Length { get; set; }

        public int Health { get { return _positions.Count(tile => tile.TileStatus == TileStatus.Ship); } }

        public bool IsVertical { get; set; }

        private Collection<Tile> _positions;

        public Collection<Tile> Positions
        {
            get { return _positions; }
            set { _positions = value; }
        }
    }
}
