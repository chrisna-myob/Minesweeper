using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class FieldBuilder : IBuild
    {
        private INumberGenerator _rng;

        public FieldBuilder(INumberGenerator rng)
        {
            _rng = rng;
        }

        public List<Coordinate> MakeUniqueMineCoordinates(int numberOfMines, Dimension dimension)
        {
            var coordinateArray = new List<Coordinate>();

            var numberOfMineCoordinates = 0;
            while (numberOfMineCoordinates < numberOfMines)
            {
                Coordinate coordinate = CreateRandomCoordinate(dimension);

                if (coordinateArray.Count == 0)
                {
                    coordinateArray.Add(coordinate);
                    numberOfMineCoordinates++;
                }
                else
                {
                    if (!coordinateArray.Contains(coordinate))
                    {
                        coordinateArray.Add(coordinate);
                    numberOfMineCoordinates++;
                    }

                }

            }

            return coordinateArray;
        }

        public Field CreateField(Dimension dimension)
        {
            var numMines = _rng.GetRandomNumber(1, dimension.NumRows*dimension.NumCols);
            
            var coordinates = MakeUniqueMineCoordinates(numMines, dimension);

            var field = MakeField(dimension, coordinates, numMines);
            CalculateHints(field, dimension);

            return new Field(dimension, numMines, field);
        }

        public Coordinate CreateRandomCoordinate(Dimension dimension)
        {
            var row = _rng.GetRandomNumber(0, dimension.NumRows);
            var column = _rng.GetRandomNumber(0, dimension.NumCols);

            var coordinate = new Coordinate(row, column);
            return coordinate;
        }

        public ISquare[,] MakeField(Dimension dimension, List<Coordinate> mineCoordinates, int numMines)
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
                for(var col = 0; col < dimension.NumCols; col++)
                {
                    if (field[row, col] == null) field[row, col] = new SafeSquare();
                }
            }

            return field;
        }

        public void CalculateHints(ISquare[,] field, Dimension dimension)
        {
            var numRows = dimension.NumRows;
            var numCols = dimension.NumCols;

            for (var row = 0; row < numRows; row++)
            {
                for (var col = 0; col < numCols; col++)
                {
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(row, col, dimension);
                    var mineCount = GetNumberOfAdjacentMines(field, adjacentSquaresList);
                    field[row, col].AddHint(mineCount);
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
