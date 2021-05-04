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
                    if (field[row,col].HasMine() == false)
                    {
                        var mineCount = 0;
                        if (row == 0 && col == 0)
                        {
                            mineCount = GetHintCountForTopLeftCorner(field, row, col);
                        }
                        else if (row == 0 && col == numCols - 1)
                        {
                            mineCount = GetHintCountForTopRightCorner(field, row, col);
                        }
                        else if (row == numRows - 1 && col == 0)
                        {
                            mineCount = GetHintCountForBottomRightCorner(field, row, col);
                        }
                        else if (row == numRows - 1 && col == numCols - 1)
                        {
                            mineCount = GetHintCountForBottomLeftCorner(field, row, col);
                        }
                        else if (row == 0)
                        {
                            mineCount = GetHintCountForTopRow(field, row, col);
                        }
                        else if (col == 0)
                        {
                            mineCount = GetHintCountForLeftColumn(field, row, col);
                        }
                        else if (col == numCols - 1)
                        {
                            mineCount = GetHintCountForRightColumn(field, row, col);
                        }
                        else if (row == numRows - 1)
                        {
                            mineCount = GetHintCountForBottomRow(field, row, col);
                        }
                        else
                        {
                            mineCount = GetHintCountForCentreSquare(field, row, col);
                        }

                        field[row, col].AddHint(mineCount);
                    }
                }
            }
        }


        private static int GetHintCountForTopLeftCorner(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row, col + 1].HasMine()) mineCount++;
            if (field[row + 1, col + 1].HasMine()) mineCount++;
            if (field[row + 1, col].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForTopRightCorner(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row, col - 1].HasMine()) mineCount++;
            if (field[row + 1, col - 1].HasMine()) mineCount++;
            if (field[row + 1, col].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForBottomLeftCorner(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row - 1, col - 1].HasMine()) mineCount++;
            if (field[row - 1, col].HasMine()) mineCount++;
            if (field[row, col - 1].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForBottomRightCorner(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row - 1, col].HasMine()) mineCount++;
            if (field[row - 1, col + 1].HasMine()) mineCount++;
            if (field[row, col + 1].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForTopRow(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row, col - 1].HasMine()) mineCount++;
            if (field[row, col + 1].HasMine()) mineCount++;
            if (field[row + 1, col - 1].HasMine()) mineCount++;
            if (field[row + 1, col].HasMine()) mineCount++;
            if (field[row + 1, col + 1].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForBottomRow(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row - 1, col - 1].HasMine()) mineCount++;
            if (field[row - 1, col].HasMine()) mineCount++;
            if (field[row - 1, col + 1].HasMine()) mineCount++;
            if (field[row, col - 1].HasMine()) mineCount++;
            if (field[row, col + 1].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForLeftColumn(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row - 1, col].HasMine()) mineCount++;
            if (field[row - 1, col + 1].HasMine()) mineCount++;
            if (field[row, col + 1].HasMine()) mineCount++;
            if (field[row + 1, col].HasMine()) mineCount++;
            if (field[row + 1, col + 1].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForRightColumn(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row - 1, col - 1].HasMine()) mineCount++;
            if (field[row - 1, col].HasMine()) mineCount++;
            if (field[row, col - 1].HasMine()) mineCount++;
            if (field[row + 1, col - 1].HasMine()) mineCount++;
            if (field[row + 1, col].HasMine()) mineCount++;
            return mineCount;
        }

        private static int GetHintCountForCentreSquare(ISquare[,] field, int row, int col)
        {
            var mineCount = 0;
            if (field[row - 1, col - 1].HasMine()) mineCount++;
            if (field[row - 1, col].HasMine()) mineCount++;
            if (field[row - 1, col + 1].HasMine()) mineCount++;
            if (field[row, col - 1].HasMine()) mineCount++;
            if (field[row, col + 1].HasMine()) mineCount++;
            if (field[row + 1, col - 1].HasMine()) mineCount++;
            if (field[row + 1, col].HasMine()) mineCount++;
            if (field[row + 1, col + 1].HasMine()) mineCount++;
            return mineCount;
        }

    }
}
