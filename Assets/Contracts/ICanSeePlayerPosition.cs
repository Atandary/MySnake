using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets.Contracts
{
    internal interface ICanSeePlayerPosition
    {
        public PointCoordinate PlayerPosition { get; }
    }
}
