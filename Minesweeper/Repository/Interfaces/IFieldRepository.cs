using System.Collections.Generic;

namespace Minesweeper
{
    public interface IFieldRepository
    {
        Dimension GetDimension();
        int NumberOfMines();
        bool CanShowSquare(Coordinate coord);
        string GetSquareValue(Coordinate coord);
        bool CoordinateHasMine(Coordinate coord);
        void SetSquareToShow(Coordinate coord);
        bool CoordinateHasHintLargerThanZero(Coordinate coord);
        List<Coordinate> GetMineCoordinates();
    }
}
