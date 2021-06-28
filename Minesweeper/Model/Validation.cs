using System;
using System.Text.RegularExpressions;

namespace Minesweeper
{
    public class Validation
    {
        public void CoordinateHasAlreadyBeenUncovered(bool result)
        {
            if (result)
            {
                throw new InvalidInputException("You have already entered this coordinate.\n");
            }
        }

        public void IsFieldDimensionInputValid(String dimensions)
        {
            if (HasNegativeNumber(dimensions))
            {
                throw new InvalidInputException("Dimension cannot be negative\n");
            }

            if (!InCorrectFormat(dimensions))
            {
                throw new InvalidInputException("Dimension must be in the format row,column with integer values\n");
            }

            if (!HasCorrectIntegerDimensions(dimensions))
            {
                throw new InvalidInputException("Dimension values must be between 1 - 100\n");
            }
        }

        public void IsCoordinateInputValid(Dimension dimension, string coordinate)
        {
            if (HasNegativeNumber(coordinate))
            {
                throw new InvalidInputException("Coordinate cannot be negative\n");
            }

            if (!InCorrectFormat(coordinate))
            {
                throw new InvalidInputException("Coordinate must be in the format row,column with integer values\n");
            }

            if (!InputIsWithinFieldBounds(dimension, coordinate))
            {
                throw new InvalidInputException("Coordinate must be within the field bounds\n");
            }
        }

        public void IsDifficultyLevelValid(string input)
        {
            if (!(input == "EASY" || input == "INTERMEDIATE" || input == "EXPERT"))
            {
                throw new InvalidInputException("That is not a valid difficulty.\n");
            }
        }

        private bool HasNegativeNumber(string dimensions)
        {
            return dimensions.Contains('-');
        }

        private bool InputIsWithinFieldBounds(Dimension dimension, string coordinate)
        {
            var coordinateArray = coordinate.Split(',');

            var x = Int32.Parse(coordinateArray[0]);
            var y = Int32.Parse(coordinateArray[1]);

            if (x > 0 && x <= dimension.NumRows && y > 0 && y <= dimension.NumCols) return true;
            return false;
        }

        private bool HasCorrectIntegerDimensions(string input)
        {
            var coordinateArray = input.Split(',');

            var row = Int32.Parse(coordinateArray[0]);
            var column = Int32.Parse(coordinateArray[1]);

            if (row - 1 <= 0 || column - 1 <= 0 || row > 100 || column > 100) return false;
            return true;
        }

        private bool InCorrectFormat(string input)
        {
            var correctFormatRegex = @"\d+,\d+";
            MatchCollection validInput = Regex.Matches(input, correctFormatRegex);
            return validInput.Count > 0;
        }

        
    }
}
