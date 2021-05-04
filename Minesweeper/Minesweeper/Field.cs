using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private int _mines;
        private ISquare[,] _field;

        public Dimension Dimension => _dimension;
        public int NumberOfMines => _mines;

        public Field(Dimension dimension, int mines, ISquare[,] field)
        {
            _dimension = dimension;
            _mines = mines;
            _field = field;
        }

        public ISquare[,] GetField()
        {
            return _field;
        }

        public ISquare GetSquareFromCoordinate(Coordinate coord)
        {
            return _field[coord.X, coord.Y];
        }

        public void SetSquareToShowWithCoordinate(Coordinate coord)
        {
            _field[coord.X, coord.Y].SetSquareToShow();
        }
    }
}
