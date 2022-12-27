using System.Collections.Generic;
using System.Collections.ObjectModel;
using Battleships.Model;

namespace Battleships
{
    public static class Constants
    {
        public const int PlayerBoardSize = 10;

        public static readonly IEnumerable<ShipModel> PlayableShips = new ReadOnlyCollection<ShipModel>(
            new List<ShipModel>
            {
                new ShipModel() { Name = "Carrier", Length = 5, Health = 5, IsVertical = true },
                new ShipModel() { Name = "Battleship", Length = 4, Health = 4, IsVertical = true },
                new ShipModel() { Name = "Cruiser", Length = 3, Health = 3, IsVertical = true },
                new ShipModel() { Name = "Submarine", Length = 3, Health = 3, IsVertical = true },
                new ShipModel() { Name = "Destroyer", Length = 2, Health = 2, IsVertical = true },
            });
    }
}
