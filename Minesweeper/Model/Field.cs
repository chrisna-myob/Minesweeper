using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private int _numberOfMines;
        private ISquare[,] _board;
        private List<Coordinate> _mineCoordinates;
        private const string NO_HINT = "0";

        public Dimension Dimension => _dimension;
        public int NumberOfMines => _numberOfMines;

        public Field(Dimension dimension, int mines, ISquare[,] board, List<Coordinate> mineCoordinates)
        {
            _dimension = dimension;
            _numberOfMines = mines;
            _board = board;
            _mineCoordinates = mineCoordinates;
        }

        public string GetSquareValue(Coordinate coord)
        {
            return _board[coord.X, coord.Y].GetSquareValue();
        }

        public bool CanShowSquare(Coordinate coord)
        {
            return _board[coord.X, coord.Y].CanShow;
        }

        public bool CoordinateHasMine(Coordinate coord)
        {
            return _board[coord.X, coord.Y].HasMine();
        }

        public void SetSquareToShowWithCoordinate(Coordinate coord)
        {
            _board[coord.X, coord.Y].SetSquareToShow();
        }

        public bool CoordinateHasHintLargerThanZero(Coordinate coord)
        {
            return _board[coord.X, coord.Y].GetSquareValue() != NO_HINT;
        }

        public List<Coordinate> GetMineCoordinates()
        {
            return _mineCoordinates;
        }
    }
}
