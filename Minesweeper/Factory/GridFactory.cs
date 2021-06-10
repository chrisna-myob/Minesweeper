using System;
using System.Collections.Generic;

namespace Minesweeper.Factory
{
    public class GridFactory
    {
        public ISquare[,] MakeGrid(Dimension dimension, List<Coordinate> mineCoordinates)
        {
            var grid = new ISquare[dimension.NumRows, dimension.NumCols];

            foreach (var mine in mineCoordinates)
            {
                grid[mine.X, mine.Y] = new MineSquare();
            }

            for (var row = 0; row < dimension.NumRows; row++)
            {
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    if (grid[row, col] == null) grid[row, col] = new SafeSquare();
                }
            }

            CalculateHintsForGrid(grid, dimension);

            return grid;
        }

        private void CalculateHintsForGrid(ISquare[,] grid, Dimension dimension)
        {
            for (var row = 0; row < dimension.NumRows; row++)
            {
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate, dimension);
                    var mineCount = GetNumberOfAdjacentMines(grid, adjacentSquaresList);
                    if (mineCount > 0) grid[row, col].AddHint(mineCount);
                }
            }
        }

        private int GetNumberOfAdjacentMines(ISquare[,] grid, List<Coordinate> AdjacentSquaresList)
        {
            var mineCount = 0;
            foreach (var coord in AdjacentSquaresList)
            {
                if (grid[coord.X, coord.Y].HasMine()) mineCount++;
            }
            return mineCount;
        }
    }
}
