using mySnake.Assets.Contracts;
using mySnake.Assets.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Text;

namespace mySnake.Assets
{
    public class GameLogic
    {
        private const int SleepTimeMs = 200;
        private const int GameFieldHeight = 20;
        private const int GameFieldWidth = 30;
        private const int DisplayInfoFieldHeight = 3;
        private const int DisplayInfoFieldWidth = GameFieldWidth;

        private readonly Borders _borderObject;
        private readonly DisplayInfo _displayInfoObject;
        //private readonly InputSystem _inputSystemObject;
        private readonly Player _playerObject;
        private readonly FoodManager _foodManager;

        private readonly List<IDrawable> _drawableCollection;
        private List<ITickable> _tickableCollection;
        private List<IDestroyable> _destroyableCollection;

        public static void EntryPoint()
        {
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            GameManager.Instance.GameStart();

            var gameLogic = new GameLogic();
            gameLogic.CoreLoop();
        }

        private GameLogic()
        {
            Console.CursorVisible = false;
            Console.Clear();

            _borderObject = new Borders(GameFieldWidth, GameFieldHeight, DisplayInfoFieldHeight);
            //TODO: Rewrite to use new Borders ex
            _displayInfoObject = new DisplayInfo(DisplayInfoFieldWidth, DisplayInfoFieldHeight);
            _playerObject = new Player(new PointCoordinate(GameFieldWidth / 2, GameFieldHeight / 2), _borderObject);
            _foodManager = new FoodManager(_playerObject, _borderObject);
   

            _tickableCollection = new List<ITickable>();
            _tickableCollection.Add(GameManager.Instance);
            _tickableCollection.Add(_playerObject);
            _tickableCollection.Add(_foodManager);
            _drawableCollection = new List<IDrawable>();
            _drawableCollection.Add(_borderObject);
            _drawableCollection.Add(_displayInfoObject);
            _drawableCollection.Add(_playerObject);
            _drawableCollection.Add(_foodManager);
            _destroyableCollection = new List<IDestroyable>();

            //MoveDirection newPlayerMoveDirection;
        }

        private void CoreLoop()
        {
            bool isExitReq = false;
            bool isPauseRequired = false;
            
            DrawFrame();

            while (!isExitReq)
            {
                DoStaticTick();

                if (isExitRequired())
                {
                    isExitReq = true;
                    break;
                }

                DoTick();

                CleaningUpDestroyedObjects();

                DrawFrame();

                Thread.Sleep(60);
            }
        }

        private void DrawFrame()
        {
            Console.Clear();
            foreach (var drawableObj in _drawableCollection)
            {
                drawableObj.Draw();
            }
        }

        private void DoStaticTick()
        {
            InputSystem.CatchInput();
        }

        private bool isExitRequired()
        {
            KeyType _pressedTypeKey;
            InputSystem.GetKeyDown(out _pressedTypeKey);

            if (_pressedTypeKey == KeyType.Esc)
            {
                Console.Clear();
                return true;
            }

            return false;
        }

        private void DoTick()
        {
            //TODO: Через ! и foreach ниже
            if (GameManager.Instance.IsGameRunning)
            {
                foreach (var tickableObj in _tickableCollection)
                {
                    tickableObj.Tick();
                }
            }
        }

        private void CleaningUpDestroyedObjects()
        {
            var candidatesOnDestroy = new List<IDestroyable>();

            foreach (var destroyableObj in _destroyableCollection)
            {
                if (!destroyableObj.NeedToBeDestroyed)
                    continue;

                candidatesOnDestroy.Add(destroyableObj);

                if (destroyableObj is IDrawable drawableObj && _drawableCollection.Contains(drawableObj))
                    _drawableCollection.Remove(drawableObj);
                if (destroyableObj is ITickable tickableObj && _tickableCollection.Contains(tickableObj))
                    _tickableCollection.Remove(tickableObj);

            }

            foreach (var candidate in candidatesOnDestroy)
                _destroyableCollection.Remove(candidate);
        }
    }
}
