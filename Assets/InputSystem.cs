using mySnake.Assets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public static class InputSystem
    {
        private static ConsoleKeyInfo _prevFramePressedKey;
        private static ConsoleKeyInfo _curFramePressedKey;
        private static ConsoleKeyInfo _rawKeyInfo;
        public static KeyType PressedKeyType { get; private set; }

        public static KeyType GetPressedKey(ConsoleKeyInfo rawKeyInfo)
        {
            return rawKeyInfo.Key switch
            {
                ConsoleKey.W or ConsoleKey.UpArrow => KeyType.MoveUp,
                ConsoleKey.S or ConsoleKey.DownArrow => KeyType.MoveDown,
                ConsoleKey.A or ConsoleKey.LeftArrow => KeyType.MoveLeft,
                ConsoleKey.D or ConsoleKey.RightArrow => KeyType.MoveRight,
                ConsoleKey.P => KeyType.Pause,
                ConsoleKey.G => KeyType.Grow,
                ConsoleKey.F => KeyType.Food,
                ConsoleKey.Escape => KeyType.Esc,
                _ => KeyType.UnknownKey
            };
        }

        public static void CatchInput()
        {
            _prevFramePressedKey = _curFramePressedKey;
            PressedKeyType = default;

            if (Console.KeyAvailable)
            {
                _curFramePressedKey = Console.ReadKey(true);
                //if (GetKeyDown(out _rawKeyInfo))
                //{
                //    PressedKeyType = GetPressedKey(_rawKeyInfo);
                //}
            }
            else
            {
                _curFramePressedKey = default;
            }
        }

        public static bool GetRawKeyDown(out ConsoleKeyInfo keyInfo)
        {
            keyInfo = default;

            if (_curFramePressedKey.Key == ConsoleKey.None) 
                return false;

            if (_prevFramePressedKey.Key == _curFramePressedKey.Key)
                return false;

            keyInfo = _curFramePressedKey;
            return true;
        }

        public static bool GetKeyDown(out KeyType pressedKey)
        {
            if (!GetRawKeyDown(out ConsoleKeyInfo rawKeyInfo))
            {
                pressedKey = default;
                return false;
            }

            pressedKey = GetPressedKey(rawKeyInfo);

            return pressedKey != KeyType.UnknownKey;
        }

        public static bool GetKeyUp(out ConsoleKeyInfo keyInfo)
        {
            keyInfo = default;

            if (_prevFramePressedKey.Key == ConsoleKey.None)
                return false;

            // TODO: если множественный инпут?
            if (_prevFramePressedKey.Key == _curFramePressedKey.Key)
                return false;

            keyInfo = _curFramePressedKey;
            return true;
        }

        public static bool GetRawKeyHold(out ConsoleKeyInfo keyInfo)
        {
            keyInfo = default;

            if (_curFramePressedKey.Key == ConsoleKey.None)
                return false;

            if (_prevFramePressedKey.Key != _curFramePressedKey.Key)
                return false;

            keyInfo = _curFramePressedKey;
            return true;
        }
        public static bool GetKeyHold(out KeyType pressedKeyType)
        {
            if (!GetRawKeyHold(out ConsoleKeyInfo rawKeyInfo))
            {
                pressedKeyType = default;
                return false;
            }

            pressedKeyType = GetPressedKey(rawKeyInfo);

            return pressedKeyType != KeyType.UnknownKey;
        }
    }
}
