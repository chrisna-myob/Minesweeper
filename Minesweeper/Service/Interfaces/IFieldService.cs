using System;

namespace Minesweeper
{
    public interface IFieldService
    {
        Dimension GetDimension();
        void HandleCoordinate(Coordinate coord);
        bool HasWon();
        bool HasLost();
        void CoordinateHasAlreadyBeenUsed(Coordinate coord);
        string UncoveredBoardToString();
        string BoardToString();
    }
}
