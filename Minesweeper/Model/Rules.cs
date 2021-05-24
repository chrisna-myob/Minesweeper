using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public static class Rules
    {
        public static bool HasWon(IFieldService fieldService)
        {
            if (fieldService.RemainingSquaresAreMines())
            {
                return true;
            }
            return false;
        }

        public static bool GameHasEnded(IFieldService fieldService)
        {
            if (fieldService.MineHasBeenUncovered() || fieldService.RemainingSquaresAreMines())
            {
                return true;
            }
            return false;
        }

        public static void CoordinateHasAlreadyBeenUsed(IFieldService fieldService, Coordinate coord)
        {
            if (fieldService.CanShowSquare(coord))
            {
                throw new InvalidInputException("You have already entered this coordinate.");
            }
        }
    }
}
