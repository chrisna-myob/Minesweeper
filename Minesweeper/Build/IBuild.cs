using System;
using System.Collections.Generic;
namespace Minesweeper
{
    public interface IBuild
    {
        List<Coordinate> MakeUniqueMineCoordinates(int numberOfMines, Dimension dimension);

        Field CreateField(Dimension dimension);

        ISquare[,] MakeBoard(Dimension dimension, List<Coordinate> mineCoordinates, int numMines);

        void CalculateHints(ISquare[,] field, Dimension dimension);

    }
}
