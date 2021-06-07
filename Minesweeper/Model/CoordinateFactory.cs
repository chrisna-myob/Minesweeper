using System;

namespace Minesweeper.Model
{
    public class CoordinateFactory
    {
        public Coordinate MakeCoordinate(Dimension dimension, string input, Validation validation)
        {
            validation.IsCoordinateInputValid(dimension, input);

            var coordinateArray = input.Split(',');

            var x = Int32.Parse(coordinateArray[0]) - 1;
            var y = Int32.Parse(coordinateArray[1]) - 1;

            return new Coordinate(x, y);
        }
    }
}
