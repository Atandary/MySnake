using mySnake.Assets.Contracts;
using mySnake.Assets.Tools;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Text;

namespace mySnake.Assets
{
    public class Player : IDrawable, ICanSeeBorders, ICanSeePlayerPosition, ITickable
    {
        private PointCoordinate _position;

        private Borders _borders;
        private int _ticksFromLastMove;
        private KeyType _insidePressedType;
        private List<PointCoordinate> _tail = new List<PointCoordinate>();
        private PointCoordinate tempTailPos;


        public MoveDirection playerMoveDirection = default;
        public MoveDirection newPlayerMoveDirection;
        public PointCoordinate _prevPosition;
        

        public PointCoordinate PlayerPosition => _position;
        public Borders Borders => _borders;

        public Player(PointCoordinate startPosition, Borders borders)
        {
            _position = startPosition;
            _borders = borders;
        }

        public void Move(MoveDirection direction)
        {
            _prevPosition = _position;

            switch (direction)
            {
                case MoveDirection.Up:
                    _position.Y = _position.Y <= _borders.Indent ? _borders.Indent : _position.Y - 1;
                    break;
                case MoveDirection.Down:
                    _position.Y = _position.Y >= _borders.Height - 2 ? _borders.Height - 2 : _position.Y + 1;
                    break;
                case MoveDirection.Left:
                    // TODO: А если отступы?
                    _position.X = _position.X <= 1 ? 1 : _position.X - 1;
                    break;
                case MoveDirection.Right:
                    _position.X = _position.X >= _borders.Width - 2 ? _borders.Width - 2 : _position.X + 1;
                    break;
                default:
                    throw new InvalidEnumArgumentException("Unknown enum direction value");
            }
            playerMoveDirection = direction;

            
            // TODO: Вынести в отдельный метод логики хвоста
            if (_tail.Count >= 1)
            {
                _tail[0] = _prevPosition;

                for (int i = 1; i < _tail.Count; i++)
                {
                    tempTailPos = _tail[i];
                    _tail[i] = _tail[i - 1];
                    _tail[i - 1] = tempTailPos;
                }
            }
        }

        public void Draw()
        {
            Console.SetCursorPosition(_position.X, _position.Y);
            Console.Write(SymbolsProvider.SnakeHead);

            foreach (var partOfTail in _tail)
            {
                Console.SetCursorPosition(partOfTail.X, partOfTail.Y);
                Console.Write(SymbolsProvider.SnakeTail);
            }
        }

        public void Tick()
        {
            _ticksFromLastMove++;

            InputSystem.GetKeyDown(out _insidePressedType);
            Grow();
            ChangeDirection();
            MovePlayer();
        }

        public void ChangeDirection()
        {
            var rawPlayerMoveDirection = MoveDirectionConverter.ConvertMoveDirectionFromKeyType(_insidePressedType);

            if (IsAllowedDirection(rawPlayerMoveDirection, playerMoveDirection))
                newPlayerMoveDirection = rawPlayerMoveDirection;
            else
                newPlayerMoveDirection = playerMoveDirection;
        }

        private bool IsAllowedDirection(MoveDirection wantedDirection, MoveDirection currentDirection)
        {
            if (wantedDirection == MoveDirection.Default) return false;
            else if (wantedDirection == currentDirection) return false;
            else if (wantedDirection == MoveDirection.Up && currentDirection == MoveDirection.Down) return false;
            else if (wantedDirection == MoveDirection.Down && currentDirection == MoveDirection.Up) return false;
            else if (wantedDirection == MoveDirection.Left && currentDirection == MoveDirection.Right) return false;
            else if (wantedDirection == MoveDirection.Right && currentDirection == MoveDirection.Left) return false;
            else return true;

        }

        public void MovePlayer()
        {
            if (newPlayerMoveDirection != MoveDirection.Default)
            {
                Move(newPlayerMoveDirection);
            }
        }

        public void Grow()
        {
            if (_insidePressedType == KeyType.Grow)
                _tail.Add(PlayerPosition);
        }
    }
}
