using Minesweeper;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using Minesweeper.Factory;
using Minesweeper.Service;

namespace MinesweeperTests
{
    public class FieldTests
    {
        private readonly Field _field2x2;
        private readonly Field _field3x3;

        public FieldTests()
        {
            var gridFactory = new GridFactory(new CoordinateService());
            var mineCoordinateList = new List<Coordinate> { new Coordinate(0, 0) };

            _field2x2 = MakeField(2, gridFactory, mineCoordinateList);

            _field3x3 = MakeField(3, gridFactory, mineCoordinateList);
        }

        private Field MakeField(int dimensionValue, GridFactory gridFactory, List<Coordinate> mineCoordinates)
        {
            var dimension = new Dimension(dimensionValue, dimensionValue);
            var grid = gridFactory.MakeGrid(dimension, mineCoordinates);
            return new Field(dimension, mineCoordinates, grid);
        }

        [Fact]
        public void SquareCanBeDisplayed_InputCoordinate_ReturnFalse()
        {
            var actual = _field2x2.SquareHasBeenUncovered(new Coordinate(0, 1));

            Assert.False(actual);
        }

        [Fact]
        public void SquareCanBeDisplayed_InputCoordinateToUncover_ReturnTrue()
        {
            _field2x2.UncoverSquare(new Coordinate(0, 1));

            var actual = _field2x2.SquareHasBeenUncovered(new Coordinate(0, 1));

            Assert.True(actual);
        }

        [Fact]
        public void SquareHasMine_InputCoordinate_ReturnTrue()
        {
            var actual = _field2x2.SquareHasMine(new Coordinate(0, 0));

            Assert.True(actual);
        }

        [Fact]
        public void SquareHasMine_InputCoordinate_ReturnFalse()
        {
            var actual = _field2x2.SquareHasMine(new Coordinate(0, 1));

            Assert.False(actual);
        }

        [Fact]
        public void GetSquareValue_InputCoordinate_ReturnStringOf1()
        {
            var expected = "1";

            var actual = _field2x2.GetSquareValue(new Coordinate(0, 1));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSquareAsString_InputCoordinateAdminView_ReturnStringOf1()
        {
            var expected = " * |";

            var actual = _field2x2.GetSquareAsString(new Coordinate(0, 0), View.ADMIN);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SquareHasNoHint_InputCoordinate_ReturnFalse()
        {
            var actual = _field3x3.SquareHasNoHint(new Coordinate(0, 1));

            Assert.False(actual);
        }

        [Fact]
        public void SquareHasNoHint_InputCoordinate_ReturnTrue()
        {
            var actual = _field3x3.SquareHasNoHint(new Coordinate(0, 2));

            Assert.True(actual);
        }

        [Fact]
        public void Field_SquaresHaveCorrectHintValue()
        {
            Assert.Equal("1", _field2x2.GetSquareValue(new Coordinate(0, 1)));
            Assert.Equal("1", _field2x2.GetSquareValue(new Coordinate(1, 0)));
            Assert.Equal("1", _field2x2.GetSquareValue(new Coordinate(1, 1)));
        }
    }
}
