using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Model
{
    public class PlayerModel
    {
        public string Name { get; set; }

        public bool IsARobot { get; set; }

        public int MoveCount { get; set; }

        public int HitCount { get; set; }

        public int CarrierCount { get; set; }

        public int BattleshipCount { get; set; }

        public int CruiserCount { get; set; }

        public int SubmarineCount { get; set; }

        public int DestroyerCount { get; set; }
    }
}
