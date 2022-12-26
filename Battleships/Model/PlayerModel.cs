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
    }
}
