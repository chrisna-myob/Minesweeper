using System;
using System.Collections.Generic;
namespace Minesweeper
{
    public interface IBuild
    {
        public List<Coordinate> MakeUniqueMineCoordinates(int numberOfMines, Dimension dimension);

        public Field CreateField(Dimension dimension);

        public ISquare[,] MakeField(Dimension dimension, List<Coordinate> mineCoordinates, int numMines);

        public void CalculateHints(ISquare[,] field, Dimension dimension);

    }
}
