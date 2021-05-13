using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public static class Rules
    {
        public static bool HasWon(Field field)
        {
            if (field.RemainingSquaresAreMines())
            {
                return true;
            }
            return false;
        }

        public static void CoordinateIsUnique(Field field, Coordinate coord)
        {
            if (field.CanShowSquare(coord))
            {
                throw new InvalidInputException("You have already used this coordinate.");
            }
        }

        public static bool CanShowIndividualCoordinateInField(Field field, Coordinate coord)
        {
            if (field.CoordinateInFieldHasHintLargerThanZero(coord))
            {
                return true;
            }
            return false;
        }

        public static bool GameHasEnded(Field fieldObject)
        {
            if (fieldObject.MineHasBeenUncovered() || fieldObject.RemainingSquaresAreMines())
            {
                return true;
            }
            return false;
        }
    }
}
