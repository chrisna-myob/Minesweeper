using System;
using System.Text.RegularExpressions;

namespace Minesweeper
{
    public static class Validate
    {
        public static void IsFieldDimensionInputValid(String dimensions)
        {
            if (HasNegativeNumber(dimensions))
            {
                throw new InvalidInputException("Dimension cannot be negative");
            }

            if (!InCorrectFormat(dimensions))
            {
                throw new InvalidInputException("Dimension must be in the format x,y with integer values");
            }

            if (!HasCorrectIntegerDimensions(dimensions))
            {
                throw new InvalidInputException("Dimension values must be larger than 0");
            }
        }

        public static void IsCoordinateInputValid(Dimension dimension, string coordinate)
        {
            if (HasNegativeNumber(coordinate))
            {
                throw new InvalidInputException("Coordinate cannot be negative");
            }

            if (!InCorrectFormat(coordinate))
            {
                throw new InvalidInputException("Coordinate must be in the format x,y with integer values");
            }

            if (!InputIsWithinFieldBounds(dimension, coordinate))
            {
                throw new InvalidInputException("Coordinate must be within the field bounds");
            }
        }
        private static bool HasNegativeNumber(string dimensions)
        {
            return dimensions.Contains('-');
        }

        private static bool InputIsWithinFieldBounds(Dimension dimension, string coordinate)
        {
            var coordinateArray = coordinate.Split(',');

            var x = Int32.Parse(coordinateArray[0]);
            var y = Int32.Parse(coordinateArray[1]);

            if (x > 0 && x <= dimension.NumRows && y > 0 && y <= dimension.NumCols) return true;
            return false;
        }

        private static bool HasCorrectIntegerDimensions(string input)
        {
            var coordinateArray = input.Split(',');

            var row = Int32.Parse(coordinateArray[0]);
            var column = Int32.Parse(coordinateArray[1]);

            if (row - 1 < 0 || column - 1 < 0) return false;
            return true;
        }

        private static bool InCorrectFormat(string input)
        {
            var correctFormatRegex = @"\d+,\d+";
            MatchCollection validInput = Regex.Matches(input, correctFormatRegex);
            return validInput.Count > 0;
        }
    }
}
