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


    }
}
