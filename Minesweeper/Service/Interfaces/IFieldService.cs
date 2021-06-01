using System;

namespace Minesweeper
{
    public interface IFieldService
    {
        Dimension GetDimension();
        void HandleCoordinate(Coordinate coord);
        bool HasWon();
        bool GameHasEnded();
        void CoordinateHasAlreadyBeenUsed(Coordinate coord);

        string UncoveredBoardToString();

        string BoardToString();
    }
}
