using System.Collections.Generic;
using System.Collections.ObjectModel;
using Battleships.Model;

namespace Battleships
{
    public static class Constants
    {
        public const int PlayerBoardSize = 10;

        /*public static readonly IEnumerable<ShipModel> PlayableShips = new ReadOnlyCollection<ShipModel>(
            new List<ShipModel>
            {
                new ShipModel() { Name = "Carrier", IsVertical = true }, // size 5
                new ShipModel() { Name = "Battleship", IsVertical = true }, // size 4
                new ShipModel() { Name = "Cruiser", IsVertical = true}, // size 3
                new ShipModel() { Name = "Submarine", IsVertical = true}, // size 3
                new ShipModel() { Name = "Destroyer", IsVertical = true}, // size 2
            });
        */
    }
}
