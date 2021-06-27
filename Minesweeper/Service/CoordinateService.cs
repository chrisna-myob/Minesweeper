using System;
using System.Collections.Generic;

namespace Minesweeper.Service
{
    public class CoordinateService
    {
        public List<Coordinate> GetAdjacentCoordinates(Coordinate coordinate, Dimension dimension)
        {
            var adjacentCoordinates = new List<Coordinate>();

            for (var row = coordinate.X - 1; row <= coordinate.X + 1; row++)
            {
                for (var col = coordinate.Y - 1; col <= coordinate.Y + 1; col++)
                {
                    if (row == coordinate.X && col == coordinate.Y) continue;
                    else if (IsCoordinateValid(row, col, dimension))
                    {
                        adjacentCoordinates.Add(new Coordinate(row, col));
                    }
                }
            }

            return adjacentCoordinates;
        }

        private bool IsCoordinateValid(int x, int y, Dimension dimension)
        {
            if ((x >= 0 && x < dimension.NumRows) && (y >= 0 && y < dimension.NumCols)) return true;
            return false;
        }
    }
}
