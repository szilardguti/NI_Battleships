using System;
using System.Collections.ObjectModel;
using System.Linq;
using Battleships.Model.Helpers;

namespace Battleships.Model
{
    public class PlayBoardModel
    {
        public Collection<Tile> Tiles { get; set; }

        public Collection<ShipModel> Ships { get; set; }

        public PlayBoardModel()
        {
            InitializeTiles();
            InitializeShips();
            PlaceShips();
        }

        public Tile GetTile(int rowIndex, int colIndex)
        {
            return Tiles.FirstOrDefault(tile => tile.X == rowIndex && tile.Y == colIndex);
        }
        public void SetTile(int rowIndex, int colIndex, TileStatus tileStatus)
        {
            Tiles.FirstOrDefault(tile => tile.X == rowIndex && tile.Y == colIndex).TileStatus = tileStatus;
        }

        private void InitializeTiles()
        {
            Tiles = null;
            Tiles = new Collection<Tile>();
            for (int i = 0; i < Constants.PlayerBoardSize; i++)
            {
                for (int j = 0; j < Constants.PlayerBoardSize; j++)
                {
                    Tiles.Add(new Tile()
                    {
                        X = i,
                        Y = j,
                        TileStatus = Model.TileStatus.Empty
                    });
                }
            }
        }

        private void InitializeShips()
        {
            Ships = null;
            Ships = new Collection<ShipModel>
            {
                new ShipModel() { Name = "Carrier", Length = 5, IsVertical = false },
                new ShipModel() { Name = "Battleship", Length = 4, IsVertical = false },
                new ShipModel() { Name = "Cruiser", Length = 3, IsVertical = true },
                new ShipModel() { Name = "Submarine", Length = 3, IsVertical = true },
                new ShipModel() { Name = "Destroyer", Length = 2, IsVertical = true }
            };
        }

        private void ClearShips()
        {
            foreach (ShipModel ship in Ships)
            {
                ship.Positions?.Clear();
            }
            foreach (Tile tile in Tiles)
            {
                tile.TileStatus = TileStatus.Empty;
            }
        }

        private bool ShipsIntersect(ShipModel ship, ShipModel ship2)
        {
            if (ship.Name == ship2.Name || ship.Positions == null || ship2.Positions == null)
            {
                return false;
            }

            foreach (Tile tile in ship.Positions)
            {
                foreach (Tile other in ship2.Positions)
                {

                    if (tile.Equals(other) || tile.IsNextTo(other))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool ShipIntersectsWithAny(ShipModel ship)
        {
            foreach (ShipModel ship2 in Ships)
            {
                if (ship2.Name == ship.Name)
                {
                    continue;
                }
                if (ShipsIntersect(ship, ship2))
                {
                    return true;
                }
            }
            return false;
        }

        private bool CanFitToCeiling(Tile tile, int length)
        {
            if (tile.Y - length >= 0)
            {
                return true;
            }
            return false;
        }

        private bool CanFitToBottom(Tile tile, int length)
        {
            if (tile.Y - length <= 9)
            {
                return true;
            }
            return false;
        }

        private bool CanFitToRight(Tile tile, int length)
        {
            if (tile.X + length <= 9)
            {
                return true;
            }
            return false;
        }

        private bool CanFitToLeft(Tile tile, int length)
        {
            if (tile.X - length >= 0)
            {
                return true;
            }
            return false;
        }

        private void RandomizeShipPosition(ShipModel ship)
        {
            ship.Positions ??= new Collection<Tile>();
            Random r = new Random();
            bool isValid;
            int x;
            int y;
            Direction direction = Direction.None;
            bool bottom = false;
            bool top = false;
            bool left = false;
            bool right = false;

            do
            {
                x = r.Next(0, 10);
                y = r.Next(0, 10);

                isValid = true;

                for (int i = 0; i < ship.Length; i++)
                {
                    Tile shipPart = new Tile()
                    {
                        X = x,
                        Y = y
                    };
                    if (i == 0)
                    {
                        if (ship.IsVertical)
                        {
                            bottom = CanFitToBottom(shipPart, ship.Length);
                            top = CanFitToCeiling(shipPart, ship.Length);
                            if (bottom && top)
                            {
                                direction = r.Next(0, 2) == 0 ? Direction.Up : Direction.Down;
                            }
                            else
                            {
                                direction = bottom ? Direction.Down : Direction.Up;
                            }
                        }
                        else
                        {
                            left = CanFitToLeft(shipPart, ship.Length);
                            right = CanFitToRight(shipPart, ship.Length);
                            if (left && right)
                            {
                                direction = r.Next(0, 2) == 0 ? Direction.Left : Direction.Right;
                            }
                            else
                            {
                                direction = left ? Direction.Left : Direction.Right;
                            }
                        }
                    }
                    ship.Positions.Add(shipPart);
                    switch (direction)
                    {
                        case Direction.Up: y--; break;
                        case Direction.Down: y++; break;
                        case Direction.Left: x--; break;
                        case Direction.Right: x++; break;
                    }
                    isValid = isValid && y >= 0 && y <= 9 && x >= 0 && x <= 9;

                    if (!isValid)
                    {
                        ship.Positions.Clear();
                        break;
                    }
                }
                if (isValid)
                {
                    if (ShipIntersectsWithAny(ship))
                    {
                        ship.Positions.Clear();
                        isValid = false;
                    }
                }
            }
            while (!isValid);

            foreach (Tile shipPart in ship.Positions)
            {
                Tiles.FirstOrDefault(tile => tile.Equals(shipPart)).TileStatus = TileStatus.Ship;
            }
        }

        public void PlaceShips()
        {
            bool isValid;
            do
            {
                isValid = true;
                ClearShips();
                foreach (ShipModel ship in Ships)
                {
                    RandomizeShipPosition(ship);
                }
                foreach (ShipModel ship in Ships)
                {
                    foreach (ShipModel ship2 in Ships)
                    {
                        if (ShipsIntersect(ship, ship2))
                        {
                            isValid = false;
                            break;
                        }
                    }
                    if (!isValid)
                    {
                        break;
                    }
                }
            }
            while (!isValid);
        }
    }
}
