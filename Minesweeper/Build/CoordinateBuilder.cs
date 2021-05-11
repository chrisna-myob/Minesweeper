using System;

namespace Minesweeper.Build
{
    public static class CoordinateBuilder
    {
        public static Coordinate MakeCoordinate(string input, Dimension dimension)
        {
            Validation.IsCoordinateInputValid(dimension, input);

            int x, y;
            var coordinateArray = input.Split(',');

            x = Int32.Parse(coordinateArray[0]) - 1;
            y = Int32.Parse(coordinateArray[1]) - 1;

            return new Coordinate(x, y);
        }
    }
}
