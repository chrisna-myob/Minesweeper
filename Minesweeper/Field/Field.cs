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

        // change variable names
        public static List<Coordinate> GetAdjacentCoordinates(int x, int y, Dimension dimension)
        {
            var adjacentCoordinates = new List<Coordinate>();

            for (var row = x - 1; row <= x + 1; row++)
            {
                for (var col = y - 1; col <= y + 1; col++)
                {
                    if (row == x && col == y) continue;
                    else if (IsCoordinateValid(row, col, dimension))
                    {
                        adjacentCoordinates.Add(new Coordinate(row, col));
                    }
                }
            }

            return adjacentCoordinates;
        }

        private static bool IsCoordinateValid(int x, int y, Dimension dimension)
        {
            if ((x >= 0 && x < dimension.NumRows) && (y >= 0 && y < dimension.NumCols)) return true;
            return false;
        }
    }
}
