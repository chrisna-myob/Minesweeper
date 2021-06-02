using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class FieldBuilder
    {
        private INumberGenerator _rng;

        public FieldBuilder(INumberGenerator rng)
        {
            _rng = rng;
        }

        public Field CreateField(string difficulty, Dimension dimension)
        {
            var numMines = GetNumberOfMines(difficulty, dimension);

            var coordinates = MakeUniqueMineCoordinates(numMines, dimension);

            var field = MakeBoard(dimension, coordinates, numMines);
            var watch = new System.Diagnostics.Stopwatch();

            CalculateHints(field, dimension);

            return new Field(dimension, numMines, field, coordinates);
        }

        private int GetNumberOfMines(string difficulty, Dimension dimension)
        {
            var numOfSquares = dimension.NumCols * dimension.NumRows;
            var percentage = Messages.mineDifficultyPercentage[difficulty];

            var numOfMines = Math.Floor(numOfSquares*percentage);

            var intOfMines = Convert.ToInt32(numOfMines);

            return intOfMines >= 1 ? intOfMines : 1;
        }

        private List<Coordinate> MakeUniqueMineCoordinates(int numberOfMines, Dimension dimension)
        {
            var coordinateArray = new List<Coordinate>();
            var numberOfMineCoordinates = 0;

            while (numberOfMineCoordinates < numberOfMines)
            {
                Coordinate coordinate = CreateRandomCoordinate(dimension);
                if (!coordinateArray.Contains(coordinate))
                {
                    coordinateArray.Add(coordinate);
                    numberOfMineCoordinates++;
                }
            }
            return coordinateArray;
        }

        private Coordinate CreateRandomCoordinate(Dimension dimension)
        {
            var row = _rng.GetRandomNumber(0, dimension.NumRows);
            var column = _rng.GetRandomNumber(0, dimension.NumCols);

            return new Coordinate(row, column);
        }

        private ISquare[,] MakeBoard(Dimension dimension, List<Coordinate> mineCoordinates, int numMines)
        {
            ISquare[,] field = new ISquare[dimension.NumRows, dimension.NumCols];

            if (mineCoordinates != null)
            {
                foreach (var mine in mineCoordinates)
                {
                    field[mine.X, mine.Y] = new MineSquare();
                }
            }

            for (var row = 0; row < dimension.NumRows; row++)
            {
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    if (field[row, col] == null) field[row, col] = new SafeSquare();
                }
            }

            return field;
        }

        private void CalculateHints(ISquare[,] field, Dimension dimension)
        {
            for (var row = 0; row < dimension.NumRows; row++)
            {
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(row, col, dimension);
                    var mineCount = GetNumberOfAdjacentMines(field, adjacentSquaresList);
                    if (mineCount > 0) field[row, col].AddHint(mineCount);
                }
            }
        }

        private int GetNumberOfAdjacentMines(ISquare[,] field, List<Coordinate> AdjacentSquaresList)
        {
            var mineCount = 0;
            foreach (var coord in AdjacentSquaresList)
            {
                if (field[coord.X, coord.Y].HasMine()) mineCount++;
            }
            return mineCount;
        }

    }
}
