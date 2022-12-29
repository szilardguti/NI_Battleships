using Battleships.Model.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Linq;

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
                new ShipModel() { Name = "Carrier", Length = 5, IsVertical = true }, // size 5
                new ShipModel() { Name = "Battleship", Length = 4, IsVertical = true }, // size 4
                new ShipModel() { Name = "Cruiser", Length = 3, IsVertical = true }, // size 3
                new ShipModel() { Name = "Submarine", Length = 3, IsVertical = true }, // size 3
                new ShipModel() { Name = "Destroyer", Length = 2, IsVertical = true }
            };
        }

        private void ClearShips()
        {
            foreach (ShipModel ship in Ships)
            {
                ship.Positions.Clear();
            }
            foreach (Tile tile in Tiles)
            {
                tile.TileStatus = TileStatus.Empty;
            }
        }

        private void ClearShipPositions(ShipModel ship)
        {
            foreach (Tile shipPart in ship.Positions)
            {
                Tiles.FirstOrDefault(tile => tile.Equals(shipPart)).TileStatus = TileStatus.Empty;
            }
            ship.Positions.Clear();
        }

        private bool ShipsIntersect(ShipModel ship, ShipModel ship2)
        {
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

        private void RandomizeShipPosition(ShipModel ship)
        {
            Random r = new Random();
            bool isValid;
            int x;
            int y;
            do
            {
                x = r.Next(0, 10);
                y = r.Next(0, 10);
                ship.IsVertical = r.Next(0, 2) == 0 ? true : false;
                isValid = true;

                for (int i = 0; i < ship.Length; i++)
                {
                    if (ship.IsVertical)
                    {
                        Tile shipPart = new Tile()
                        {
                            X = x,
                            Y = y--
                        };
                        ship.Positions.Add(shipPart);
                    }
                    else
                    {
                        Tile shipPart = new Tile()
                        {
                            X = x++,
                            Y = y
                        };
                        ship.Positions.Add(shipPart);
                    }

                    isValid = y >= 0 && y <= 9 && x >= 0 && x <= 9;

                    if (!isValid)
                    {
                        ship.Positions.Clear();
                        break;
                    }
                }
            }
            while (!isValid);

            foreach (Tile shipPart in ship.Positions)
            {
                Tiles.FirstOrDefault(tile => tile.Equals(shipPart)).TileStatus = TileStatus.Ship;
            }
        }

        private void PlaceShips()
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
