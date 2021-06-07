using System;
using System.Collections.Generic;

namespace Minesweeper.Model
{
    public class MineCoordinateFactory
    {
        private INumberGenerator _rng;

        public MineCoordinateFactory(INumberGenerator rng)
        {
            _rng = rng;
        }

        public List<Coordinate> MakeUniqueMineCoordinates(DifficultyLevel difficulty, Dimension dimension)
        {
            var numberOfMines = GetNumberOfMines(difficulty, dimension);

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

        private int GetNumberOfMines(DifficultyLevel difficulty, Dimension dimension)
        {
            var numOfSquares = dimension.NumCols * dimension.NumRows;
            var percentage = Messages.mineDifficultyPercentage[difficulty];

            var numOfMines = Math.Floor(numOfSquares * percentage);

            var intOfMines = Convert.ToInt32(numOfMines);

            return intOfMines >= 1 ? intOfMines : 1;
        }
    }
}
