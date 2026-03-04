using mySnake.Assets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public class DisplayInfo : IDrawable, ITickable
    {
        public int Width { get; private set; }
        public int Height { get; private set; }
        public string Info { get; private set; }

        public DisplayInfo(int width, int height, string info = "")
        {
            Width = width;
            Height = height;
            Info = info;
        }

        public void Draw()
        {
            for (var x = 0; x < Width; x++)
            {
                Console.SetCursorPosition(x, 0);
                Console.Write(SymbolsProvider.DisplayBorder);
                //Console.SetCursorPosition(x, Height - 1);
                //Console.Write(SymbolsProvider.DisplayBorder);
                Console.SetCursorPosition(Width / 2 - Info.Length / 2, Height / 2);
                Console.Write(Info);
            }

            for (var y = 1; y < Height - 1; y++)
            {
                Console.SetCursorPosition(0, y);
                Console.Write(SymbolsProvider.DisplayBorder);
                Console.SetCursorPosition(Width - 1, y);
                Console.Write(SymbolsProvider.DisplayBorder);
            }
        }

        public void DisplayOnRunning()
        {
            Info = "Game is Running...";
        }

        public void DisplayOnPause()
        {
            Info = "Pause";
        }

        public void Tick()
        {
            throw new NotImplementedException();
        }
    }
}
