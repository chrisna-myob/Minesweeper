using System.Collections.Generic;
using Minesweeper;
using Minesweeper.Factory;
using Xunit;

namespace MinesweeperTests
{
    public class GridFactoryTests
    {
        [Fact]
        public void MakeGrid_InputDimensionAndMineCoordinates_ReturnGridObjectToValidateSquareTypes()
        {
            var dimension = new Dimension(2, 2);
            var mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            var expected = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };
            var gridFactory = new GridFactory();

            var actual = gridFactory.MakeGrid(dimension, mineCoordinates);

            Assert.Equal(expected[0, 0].GetType(), actual[0, 0].GetType());
            Assert.Equal(expected[0, 1].GetType(), actual[0, 1].GetType());
            Assert.Equal(expected[1, 0].GetType(), actual[1, 0].GetType());
            Assert.Equal(expected[1, 1].GetType(), actual[1, 1].GetType());
        }

        [Fact]
        public void MakeGrid_InputDimensionAndMineCoordinates_ReturnGridObjectToValidateSquareHints()
        {
            var dimension = new Dimension(2, 2);
            var mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            var expected = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };
            var gridFactory = new GridFactory();

            var actual = gridFactory.MakeGrid(dimension, mineCoordinates);

            Assert.Equal("*", actual[0, 0].GetSquareValue());
            Assert.Equal("1", actual[0, 1].GetSquareValue());
            Assert.Equal("1", actual[1, 0].GetSquareValue());
            Assert.Equal("1", actual[1, 1].GetSquareValue());
        }
    }
}
