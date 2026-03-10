using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public class FoodObject
    {
        public PointCoordinate FoodCoordinate { get; set; }
        public bool IsBitten { get; set; } = false;

        public FoodObject(PointCoordinate foodCoordinate)
        {
            FoodCoordinate = foodCoordinate;
        }
    }
}
