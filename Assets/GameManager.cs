using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public static class GameManager
    {
        public static bool IsGameRunning { get; private set; }

        public static void GameStart()
        {
            IsGameRunning = true;
        }
        public static void GameOver()
        {
            Console.SetCursorPosition(25, 25);
            Console.WriteLine("UMER");
            //IsGameRunning = false;
        }



    }
}
