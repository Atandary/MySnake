using mySnake.Assets.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace mySnake.Assets
{
    public class Tail: IDrawable, ITickable
    {
        public PointCoordinate _playerPrevPosition;
        public PointCoordinate _talePosition;
        public PointCoordinate _talePrevPosition;
        private Player _playerLink;

        private int _ticksFromLastMove;


        public Tail(Player playerLink)
        {
            _playerLink = playerLink;
        }

        public void Follow()
        {
            _talePrevPosition = _talePosition;
            _talePosition = _playerLink._prevPosition;

            // TODO: [BUG] Почему?
            //if (_talePosition.X != _playerLink.PlayerPosition.X && _talePosition.Y != _playerLink.PlayerPosition.Y)
            //    _talePosition = _playerLink._prevPosition;
        }

        public void Draw()
        {
            Console.SetCursorPosition(_talePosition.X, _talePosition.Y);
            Console.Write(SymbolsProvider.SnakeTail);
        }

        public void Tick()
        {
            _ticksFromLastMove++;
            Follow();
        }
    }
}
