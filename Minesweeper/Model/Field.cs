using System;
using System.Collections.Generic;
using Minesweeper.Model;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private int _numberOfMines;
        private ISquare[,] _board;
        private Square[,] _board2;
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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Field f = (Field)obj;
                if (NumberOfMines == f.NumberOfMines)
                {
                    for (var index = 0; index < _mineCoordinates.Count; index++)
                    {
                        if (_mineCoordinates[index].X != f._mineCoordinates[index].X && _mineCoordinates[index].Y != f._mineCoordinates[index].Y) return false;
                    }
                    return (Dimension.NumRows == f.Dimension.NumRows) && (Dimension.NumCols == f.Dimension.NumCols);
                }
                return false;
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
