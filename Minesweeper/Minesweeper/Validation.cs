using System;
using System.Text.RegularExpressions;

namespace Minesweeper
{
    public static class Validation
    {
        private const string QUIT_GAME = "q";

        public static bool IsFieldDimensionInputValid(String dimensions)
        {
            if (HasNegativeNumber(dimensions))
            {
                return false;
            }
            if (!InCorrectFormat(dimensions)) return false;

            var coordinateArray = dimensions.Split(',');

            int row;
            int column;

            var rowResult = Int32.TryParse(coordinateArray[0], out row);
            var columnResult = Int32.TryParse(coordinateArray[1], out column);

            if (rowResult == false || columnResult == false) return false;

            return HasCorrectIntegerDimensions(row, column);
        }

        public static bool IsCoordinateInputValid(Dimension dimension, string coordinate)
        {
            if (coordinate == QUIT_GAME) return true;

            if (HasNegativeNumber(coordinate))
            {
                return false;
            }

            if (!InCorrectFormat(coordinate)) return false;

            var coordinateArray = coordinate.Split(',');

            int x, y;

            var xResult = Int32.TryParse(coordinateArray[0], out x);
            var yResult = Int32.TryParse(coordinateArray[1], out y);

            if (xResult == false || yResult == false) return false;

            return HasCoordinateWithinFieldBounds(dimension, x, y);
        }

        private static bool HasCoordinateWithinFieldBounds(Dimension dimension, int x, int y)
        {

            if (x > 0 && x <= dimension.NumRows && y > 0 && y <= dimension.NumCols)
            {
                return true;
            }

            return false;
        }

        private static bool HasNegativeNumber(string dimensions)
        {
            return dimensions.Contains('-');
        }

        private static bool HasCorrectIntegerDimensions(int x, int y)
        {
            if (x - 1 < 0 || y - 1 < 0) return false;
            return true;
        }

        private static bool InCorrectFormat(string input)
        {
            var correctFormatRegex = @"\d+,\d+";
            MatchCollection validInput = Regex.Matches(input, correctFormatRegex);
            return validInput.Count > 0;
        }

        private static bool ValidateInputParse(string input)
        {
            var inputArray = input.Split(',');

            var firstResult = Int32.TryParse(inputArray[0], out _);
            var secondResult = Int32.TryParse(inputArray[1], out _);

            if (firstResult == false || secondResult == false) return false;
            else return true;
        }
    }
}
