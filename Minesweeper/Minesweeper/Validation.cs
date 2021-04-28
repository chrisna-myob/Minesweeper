using System;
using System.Text.RegularExpressions;

namespace Minesweeper
{
    public static class Validation
    {
        public static bool IsFieldDimensionInputValid(String dimensions)
        {
            if (HasNegativeNumber(dimensions))
            {
                return false;
            }

            if (!InCorrectFormat(dimensions)) return false;

            var coordinateArray = dimensions.Split(',');

            int row, column;

            var rowResult = Int32.TryParse(coordinateArray[0], out row);
            var columnResult = Int32.TryParse(coordinateArray[0], out column);

            if (rowResult == false && columnResult == false) return false;

            return HasCorrectIntegerDimensions(row, column);
        }

        public static bool IsCoordinateInputValid(Field fieldObject, string coordinate)
        {
            if (HasNegativeNumber(coordinate))
            {
                return false;
            }

            if (!InCorrectFormat(coordinate)) return false;

            var coordinateArray = coordinate.Split(',');

            int x, y;

            var xResult = Int32.TryParse(coordinateArray[0], out x);
            var yResult = Int32.TryParse(coordinateArray[0], out y);

            if (xResult == false && yResult == false) return false;

            return HasCoordinateWithinFieldBounds(fieldObject, x, y);
        }

        private static bool HasCoordinateWithinFieldBounds(Field fieldObject, int x, int y)
        {
            if (x >= 0 && x < fieldObject.NumberOfRows && y >= 0 && y < fieldObject.NumberOfColumns)
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
            return x > 0 && y > 0;
        }

        private static bool InCorrectFormat(string input)
        {
            var correctFormatRegex = @"\d+,\d+";
            MatchCollection validInput = Regex.Matches(input, correctFormatRegex);
            return validInput.Count > 0;
        }
    }
}
