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

        public Field(Dimension dimension, int mines, ISquare[,] board, List<Coordinate> mineCoordinates)
        {
            _dimension = dimension;
            _numberOfMines = mines;
            _board = board;
            _mineCoordinates = mineCoordinates;
        }
        
        public ISquare[,] GetBoard()
        {
            return _board;
        }

        public bool CanShowSquare(Coordinate coord)
        {
            return _board[coord.X, coord.Y].CanShow;
        }

        private bool CoordinateHasMine(Coordinate coord)
        {
            return _board[coord.X, coord.Y].HasMine();
        }

        public void SetSquareToShowWithCoordinate(Coordinate coord)
        {
            _board[coord.X, coord.Y].SetSquareToShow();
        }

        public bool RemainingSquaresAreMines()
        {
            var countOfMines = 0;
            for (var row = 0; row < _dimension.NumRows; row++)
            {
                for (var col = 0; col < _dimension.NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    if (!CanShowSquare(coordinate))
                    {
                        if (!CoordinateHasMine(coordinate)) return false;
                        else if (CoordinateHasMine(coordinate)) countOfMines++;
                    }
                    
                }
            }
            if (countOfMines == _numberOfMines) return true;
            return false;
        }

        public bool MineHasBeenUncovered()
        {
            foreach(var coord in _mineCoordinates)
            {
                if (_board[coord.X, coord.Y].CanShow) return true;
            }
            return false;
        }

        public bool CoordinateHasHintLargerThanZero(Coordinate coord)
        {
            return _board[coord.X, coord.Y].GetSquareValue() != NO_HINT;
        }

        public void SetAdjacentCoordinatesInFieldToShow(Coordinate coordinate)
        {
            if (CanShowSquare(coordinate)) return;
            else
            {
                _board[coordinate.X, coordinate.Y].SetSquareToShow();

                if (CoordinateHasHintLargerThanZero(coordinate)) return;
                else
                {
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate.X, coordinate.Y, _dimension);

                    foreach (var coord in adjacentSquaresList)
                    {
                        SetAdjacentCoordinatesInFieldToShow(coord);
                    }
                }
            }
        }
    }
}
