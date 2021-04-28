using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public static class FieldBuilder
    {
        public static List<Coordinate> MakeUniqueMineCoordinates(INumberGenerator rng, int numberOfMines, int numberOfRows, int numberOfColumns)
        {
            var coordinateArray = new List<Coordinate>();

            var numberOfMineCoordinates = 0;
            while (numberOfMineCoordinates < numberOfMines)
            {
                Coordinate coordinate = CreateRandomCoordinate(rng, numberOfRows, numberOfColumns);

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

        private static Coordinate CreateRandomCoordinate(INumberGenerator rng, int numberOfRows, int numberOfColumns)
        {
            var row = rng.GetRandomNumber(0, numberOfRows);
            var column = rng.GetRandomNumber(0, numberOfColumns);

            var coordinate = new Coordinate(row, column);
            return coordinate;
        }

        public static ISquare[,] MakeField(int numRows, int numColumns, List<Coordinate> mineCoordinates, int numMines)
        {
            ISquare[,] field = new ISquare[numRows, numColumns];

            if (mineCoordinates != null)
            {
                foreach (var mine in mineCoordinates)
                {
                    field[mine.X, mine.Y] = new MineSquare();
                }
            }

            for (var row = 0; row < numRows; row++)
            {
                for(var col = 0; col < numColumns; col++)
                {
                    if (field[row, col] == null) field[row, col] = new SafeSquare();
                }
            }

            return field;
        }

        public static void CalculateHints(ISquare[,] field, int numRows, int numCols)
        {
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
