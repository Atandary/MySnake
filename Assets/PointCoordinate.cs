using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public struct PointCoordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public PointCoordinate(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
