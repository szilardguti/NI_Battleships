using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Media;

namespace Battleships.Model.Helpers
{
    public class TileItem
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public SolidColorBrush Color { get; set; }
    }
}
