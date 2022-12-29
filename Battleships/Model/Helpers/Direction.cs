﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Battleships.Model.Helpers
{
    [Flags]
    public enum Direction
    {
        None = 0,
        Up = 1,
        Down = 2,
        Left = 3,
        Right = 4
    }
}
