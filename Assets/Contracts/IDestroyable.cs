using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets.Contracts
{
    public interface IDestroyable
    {
        public bool NeedToBeDestroyed { get; }
    }
}
