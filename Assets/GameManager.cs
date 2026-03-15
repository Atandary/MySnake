using mySnake.Assets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    //public static class GameManager
    //{
    //    public static bool IsGameRunning { get; private set; }

    //    public static void GameStart()
    //    {
    //        IsGameRunning = true;
    //    }
    //    public static void GameOver()
    //    {
    //        Console.SetCursorPosition(25, 25);
    //        Console.WriteLine("UMER");
    //        //IsGameRunning = false;
    //    }

    //    //Через DoStaticTick в GameLogic Core или Singlw
    //    public static void Tick()
    //    {
    //        throw new NotImplementedException();
    //    }
//}

public class GameManager : ITickable
    {
        private static readonly GameManager _instance = new GameManager();
        public static GameManager Instance {  get { return _instance; } }

        public bool IsGameRunning { get; private set; }

        public void GameStart()
        {
            IsGameRunning = true;
        }
        public void GameOver()
        {
            Console.SetCursorPosition(25, 25);
            Console.WriteLine("UMER");
            //IsGameRunning = false;
        }

        //TODO: isPause, isExitReq сюда

        //Через DoStaticTick в GameLogic Core или Singlw
        public void Tick()
        {
            throw new NotImplementedException();
        }
    }

}
