using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets.Tools
{
    public static class MoveDirectionConverter
    {
        public static MoveDirection ConvertMoveDirectionFromKeyType(KeyType key)
        {
            return key switch
            {
                KeyType.MoveUp => MoveDirection.Up,
                KeyType.MoveDown => MoveDirection.Down,
                KeyType.MoveLeft => MoveDirection.Left,
                KeyType.MoveRight => MoveDirection.Right,
                _=> MoveDirection.Default
            };
        }
    }
}
