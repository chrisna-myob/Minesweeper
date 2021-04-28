using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private int _rows;
        private int _columns;
        private int _mines;
        private ISquare[,] _field;

        public int NumberOfRows => _rows;
        public int NumberOfColumns => _columns;
        public int NumberOfMines => _mines;

        public Field(int rows, int columns, int mines, ISquare[,] field)
        {
            _rows = rows;
            _columns = columns;
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
