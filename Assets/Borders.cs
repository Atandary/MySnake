using mySnake.Assets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public class Borders : IDrawable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public int Indent { get; private set; } //NOTE: MarginUp

        public Borders(int width, int height, int indent)
        {
            Width = width;
            Height = height;
            Indent = indent;
        }

        public void Draw()
        {
            for (var x = 0; x < Width; x++)
            {
                Console.SetCursorPosition(x, Indent - 1);
                Console.Write(SymbolsProvider.Border);
                Console.SetCursorPosition(x, Height - 1);
                Console.Write(SymbolsProvider.Border);
            }

            for (var y = Indent - 1; y < Height - 1; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(SymbolsProvider.Border);
                Console.SetCursorPosition(Width - 1, y);
                Console.Write(SymbolsProvider.Border);
            }
        }
    }
}
