using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private int _mines;
        private ISquare[,] _field;
        private List<Coordinate> _mineCoordinates;
        private const string NO_HINT = "0";

        public Dimension Dimension => _dimension;
        public int NumberOfMines => _mines;

        public Field(Dimension dimension, int mines, ISquare[,] field, List<Coordinate> mineCoordinates)
        {
            _dimension = dimension;
            _mines = mines;
            _field = field;
            _mineCoordinates = mineCoordinates;
        }

        public ISquare[,] GetField()
        {
            return _field;
        }

        public bool CanShowSquare(Coordinate coord)
        {
            return _field[coord.X, coord.Y].CanShow;
        }

        private bool CoordinateHasMine(Coordinate coord)
        {
            return _field[coord.X, coord.Y].HasMine();
        }

        public void SetSquareToShowWithCoordinate(Coordinate coord)
        {
            _field[coord.X, coord.Y].SetSquareToShow();
        }

        public bool RemainingSquaresAreMines()
        {
            var countOfMines = 0;
            for (var row = 0; row < _dimension.NumRows; row++)
            {
                for (var col = 0; col < _dimension.NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    if (!CanShowSquare(coordinate) && !CoordinateHasMine(coordinate))
                    {
                        return false;
                    }
                    if (!CanShowSquare(coordinate) && CoordinateHasMine(coordinate)) countOfMines++;
                }
            }

            if (countOfMines == _mines) return true;
            else return false;
        }

        public bool MineHasBeenUncovered()
        {
            foreach(var coord in _mineCoordinates)
            {
                if (_field[coord.X, coord.Y].CanShow)
                {
                    return true;
                }
            }

            return false;
        }

        public bool CoordinateInFieldHasHintLargerThanZero(Coordinate coord)
        {
            if (_field[coord.X, coord.Y].GetSquareValue() == NO_HINT)
            {
                return false;
            }
            return true;
        }

        public void SetAdjacentCoordinatesInFieldToShow(Coordinate coordinate)
        {
            if (CanShowSquare(coordinate)) return;
            else if (!CanShowSquare(coordinate) && CoordinateInFieldHasHintLargerThanZero(coordinate))
            {
                _field[coordinate.X, coordinate.Y].SetSquareToShow();
                return;
            }
            else
            {
                _field[coordinate.X, coordinate.Y].SetSquareToShow();

                var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate.X, coordinate.Y, _dimension);

                foreach (var coord in adjacentSquaresList)
                {
                    SetAdjacentCoordinatesInFieldToShow(coord);
                }
            }
        }
    }
}
