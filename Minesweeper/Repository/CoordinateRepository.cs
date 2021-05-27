using System;
using Minesweeper.Repository.Interfaces;

namespace Minesweeper.Model
{
    public class CoordinateRepository : ICoordinateRepository
    {
        public Coordinate MakeCoordinate(string input, Dimension dimension)
        {
            Validate.IsCoordinateInputValid(dimension, input);

            var coordinateArray = input.Split(',');

            var x = Int32.Parse(coordinateArray[0]) - 1;
            var y = Int32.Parse(coordinateArray[1]) - 1;

            return new Coordinate(x, y);
        }
    }
}
