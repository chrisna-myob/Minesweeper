using System;

namespace Minesweeper
{
    public interface IFieldService
    {
        void SetAdjacentCoordinatesInFieldToShow(Coordinate coordinate);
        string ToString();
        bool RemainingSquaresAreMines();
        bool MineHasBeenUncovered();
        string UncoveredBoardToString();
        Dimension GetDimension();
        bool CanShowSquare(Coordinate coord);
    }
}
