using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Model
{
    [Flags]
    public enum TileStatus
    {
        None = 0,
        Empty = 1,
        Ship = 2,
        MissShot = 3,
        HitShot = 4
    }
}
