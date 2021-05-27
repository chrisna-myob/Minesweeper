using System;

namespace Minesweeper.Repository.Interfaces
{
    public interface ICoordinateRepository
    {
        Coordinate MakeCoordinate(string input, Dimension dimension);
    }
}
