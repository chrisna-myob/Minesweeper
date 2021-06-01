using System.Collections.Generic;

namespace Minesweeper
{
    public interface IFieldRepository
    {
        Dimension GetDimension();
        bool CanShowSquare(Coordinate coord);
        void SetCoordinateToShow(Coordinate coord);

        bool RemainingSquaresAreMines();

        bool MineHasBeenUncovered();

        string UncoveredBoardToString();

        string BoardToString();
    }
}
