using System.Collections.Generic;

namespace Minesweeper
{
    public static class GlobalHelpers
    {
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
