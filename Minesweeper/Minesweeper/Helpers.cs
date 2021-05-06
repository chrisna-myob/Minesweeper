using System.Collections.Generic;

namespace Minesweeper
{
    public static class Helpers
    {
        public static List<Coordinate> GetAdjacentCoordinates(int x, int y, Dimension dimension)
        {
            var numberOfColumns = dimension.NumCols;
            var numberOfRows = dimension.NumRows;
            var adjacentCoordinates = new List<Coordinate>();

            if (x - 1 >= 0 && y - 1 >= 0) adjacentCoordinates.Add(new Coordinate(x - 1, y - 1));
            if (x - 1 >= 0 && y + 1 < numberOfColumns) adjacentCoordinates.Add(new Coordinate(x - 1, y + 1));
            if (x + 1 < numberOfRows && y - 1 >= 0) adjacentCoordinates.Add(new Coordinate(x + 1, y - 1));
            if (x + 1 < numberOfRows && y + 1 < numberOfColumns) adjacentCoordinates.Add(new Coordinate(x + 1, y + 1));
            if (x - 1 >= 0) adjacentCoordinates.Add(new Coordinate(x - 1, y));
            if (y - 1 >= 0) adjacentCoordinates.Add(new Coordinate(x, y - 1));
            if (x + 1 < numberOfRows) adjacentCoordinates.Add(new Coordinate(x + 1, y));
            if (y + 1 < numberOfColumns) adjacentCoordinates.Add(new Coordinate(x, y + 1));

            return adjacentCoordinates;
        }
    }
}
