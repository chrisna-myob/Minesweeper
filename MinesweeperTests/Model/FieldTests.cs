using Minesweeper;
using Xunit;
using Moq;
using System;
using System.Collections.Generic;
using Minesweeper.Factory;

namespace MinesweeperTests
{
    public class FieldTests
    {
        private readonly GridFactory _gridFactory;
        private readonly List<Coordinate> mineCoordinateList;

        public FieldTests()
        {
            _gridFactory = new GridFactory();
            mineCoordinateList = new List<Coordinate> { new Coordinate(0, 0) };
        }

        [Fact]
        public void SquareCanBeDisplayed_InputCoordinate_ReturnFalse()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);

            var actual = field.SquareHasBeenUncovered(new Coordinate(0, 1));

            Assert.False(actual);
        }

        [Fact]
        public void SquareCanBeDisplayed_InputCoordinateToUncover_ReturnTrue()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);
            field.UncoverSquare(new Coordinate(0, 1));

            var actual = field.SquareHasBeenUncovered(new Coordinate(0, 1));

            Assert.True(actual);
        }

        [Fact]
        public void SquareHasMine_InputCoordinate_ReturnTrue()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);

            var actual = field.SquareHasMine(new Coordinate(0, 0));

            Assert.True(actual);
        }

        [Fact]
        public void SquareHasMine_InputCoordinate_ReturnFalse()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);

            var actual = field.SquareHasMine(new Coordinate(0, 1));

            Assert.False(actual);
        }

        [Fact]
        public void GetSquareValue_InputCoordinate_ReturnStringOf1()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);
            var expected = "1";

            var actual = field.GetSquareValue(new Coordinate(0, 1));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetSquareAsString_InputCoordinateAdminView_ReturnStringOf1()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);
            var expected = " * |";

            var actual = field.GetSquareAsString(new Coordinate(0, 0), View.ADMIN);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void SquareHasNoHint_InputCoordinate_ReturnFalse()
        {
            var dimension = new Dimension(3, 3);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);

            var actual = field.SquareHasNoHint(new Coordinate(0, 1));

            Assert.False(actual);
        }

        [Fact]
        public void SquareHasNoHint_InputCoordinate_ReturnTrue()
        {
            var dimension = new Dimension(3, 3);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);

            var field = new Field(dimension, mineCoordinateList, grid);

            var actual = field.SquareHasNoHint(new Coordinate(0, 2));

            Assert.True(actual);
        }

        [Fact]
        public void Field_SquaresHaveCorrectHintValue()
        {
            var dimension = new Dimension(2, 2);
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinateList);
            var field = new Field(dimension, mineCoordinateList, grid);

            Assert.Equal("1", field.GetSquareValue(new Coordinate(0, 1)));
            Assert.Equal("1", field.GetSquareValue(new Coordinate(1, 0)));
            Assert.Equal("1", field.GetSquareValue(new Coordinate(1, 1)));
        }
    }
}
