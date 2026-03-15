using mySnake.Assets.Contracts;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace mySnake.Assets
{
    public class FoodManager: ICanSeePlayerPosition, ICanSeeBorders, ITickable, IDrawable
    {
        
        private PointCoordinate _playerPosition;
        private Borders _borders;
        private int _tickableCount;
        private KeyType _pressedType;
        private ICanSeePlayerPosition _playerPositionProvider;
        private int randomFoodX;
        private int randomFoodY;
        Random random = new Random();

        public List<FoodObject> FoodList = new List<FoodObject>();


        public PointCoordinate PlayerPosition => _playerPosition;
        public Borders Borders => _borders;

        public FoodManager(ICanSeePlayerPosition playerPosition, Borders borders)
        {
            _playerPositionProvider = playerPosition;
            _borders = borders;
        }

        public void Draw()
        {
            foreach (var foodObject in FoodList)
            {
                Console.SetCursorPosition(foodObject.FoodCoordinate.X, foodObject.FoodCoordinate.Y);
                Console.Write(SymbolsProvider.FoodObject);
            }
        }

        public void Tick()
        {
            _tickableCount++;
            InputSystem.GetKeyDown(out _pressedType);
            if (_pressedType != KeyType.Food)
                return;
            _playerPosition.X = _playerPositionProvider.PlayerPosition.X;
            _playerPosition.Y = _playerPositionProvider.PlayerPosition.Y;
            SpawnFood();
        }

        public void SpawnFood()
        {
            //int randomFoodX = _playerPosition.X;
            //int randomFoodY = _playerPosition.Y;
            //while (randomFoodX == _playerPosition.X && randomFoodY == _playerPosition.Y)
            //{
            //    randomFoodX = random.Next(_borders.Height);
            //    randomFoodY = random.Next(_borders.Indent, _borders.Width);
            //}
            bool _isCorrectFoodPosition = false;

            while (!_isCorrectFoodPosition)
                //TODO: Список доступных точек (все поле - змея - бордеры - еда)
                GetRandomRawFoodPosition(out _isCorrectFoodPosition);


            PointCoordinate foodPosition = new PointCoordinate(randomFoodY, randomFoodX);
            FoodList.Add(new FoodObject(foodPosition));
        }

        public void GetRandomRawFoodPosition(out bool correctPos)
        {
            bool isRawRandomInList = false;

            randomFoodX = random.Next(_borders.Indent, _borders.Height - 1);
            randomFoodY = random.Next(1, _borders.Width - 1);

            PointCoordinate randomFoodPosition = new PointCoordinate(randomFoodX, randomFoodY);

            foreach (var food in FoodList)
            {
                if (food.FoodCoordinate.X == randomFoodPosition.X && food.FoodCoordinate.Y == randomFoodPosition.Y)
                    isRawRandomInList = true;
            }

            if (randomFoodX == _playerPosition.X && randomFoodY == _playerPosition.Y)
                correctPos= false;
            else if (isRawRandomInList)
                correctPos = false;
            else
                correctPos = true;
        }
    }
}
