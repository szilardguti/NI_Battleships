using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Model
{
    public class ShipModel
    {
        public string Name { get; set; }

        public int Length { get; set; }

        public int Health { get; set; }

        public bool IsVertical { get; set; }
    }
}
