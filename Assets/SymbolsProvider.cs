using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public class SymbolsProvider
    {
        public static char Border { get; } = '*';
        public static char DisplayBorder { get; } = '^';
        public static char SnakeHead { get; } = '#';
        public static char SnakeTail { get; } = '@';
    }
}
