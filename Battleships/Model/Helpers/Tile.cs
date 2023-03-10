using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Model.Helpers
{
    public class Tile
    {
        private int _x;
        private int _y;
        private TileStatus _tileStatus;
        public int X
        {
            get { return _x; }
            set { _x = value; }
        }

        public int Y
        {
            get { return _y; }
            set { _y = value; }
        }

        public TileStatus TileStatus
        {
            get { return _tileStatus; }
            set { _tileStatus = value; }
        }

        public bool Equals(Tile other)
        {
            if (this.X == other.X && this.Y == other.Y)
            {
                return true;
            }
            return false;
        }

        public bool IsNextTo(Tile other)
        {
            if ((this.X - 1 == other.X && this.Y - 1 == other.Y) ||
                (this.X == other.X && this.Y - 1 == other.Y) ||
                (this.X + 1 == other.X && this.Y - 1 == other.Y) ||
                (this.X - 1 == other.X && this.Y == other.Y) ||
                (this.X + 1 == other.X && this.Y == other.Y) ||
                (this.X - 1 == other.X && this.Y + 1 == other.Y) ||
                (this.X == other.X && this.Y + 1 == other.Y) ||
                (this.X + 1 == other.X && this.Y + 1 == other.Y))
            {
                return true;
            }
            return false;
        }
    }
}
