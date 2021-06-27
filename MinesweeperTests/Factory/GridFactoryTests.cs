using System.Collections.Generic;
using Minesweeper;
using Minesweeper.Factory;
using Minesweeper.Service;
using Xunit;

namespace MinesweeperTests
{
    public class GridFactoryTests
    {
        private readonly Dimension _dimension;
        private readonly List<Coordinate> _mineCoordinates;
        private readonly GridFactory _gridFactory;

        public GridFactoryTests()
        {
            _dimension = new Dimension(2, 2);
            _mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            _gridFactory = new GridFactory(new CoordinateService());
        }

        [Fact]
        public void MakeGrid_InputDimensionAndMineCoordinates_ReturnGridObjectToValidateSquareTypes()
        {
            var expected = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };

            var actual = _gridFactory.MakeGrid(_dimension, _mineCoordinates);

            Assert.Equal(expected[0, 0].GetType(), actual[0, 0].GetType());
            Assert.Equal(expected[0, 1].GetType(), actual[0, 1].GetType());
            Assert.Equal(expected[1, 0].GetType(), actual[1, 0].GetType());
            Assert.Equal(expected[1, 1].GetType(), actual[1, 1].GetType());
        }

        [Fact]
        public void MakeGrid_InputDimensionAndMineCoordinates_ReturnGridObjectToValidateSquareHints()
        {
            var actual = _gridFactory.MakeGrid(_dimension, _mineCoordinates);

            Assert.Equal("*", actual[0, 0].GetSquareValue());
            Assert.Equal("1", actual[0, 1].GetSquareValue());
            Assert.Equal("1", actual[1, 0].GetSquareValue());
            Assert.Equal("1", actual[1, 1].GetSquareValue());
        }
    }
}
