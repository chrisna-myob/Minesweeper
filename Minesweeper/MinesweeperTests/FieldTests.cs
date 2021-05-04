using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class FieldTests
    {
        [Fact]
        public void Field_InputRowsColumnsListOfMineCoordinatesAndNumberOfMines_ReturnFieldObject()
        {
            var dimensions = new Dimension(2, 2);
            var expected = new Field(dimensions, 1, null);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var actual = builder.CreateField(dimensions);

            Assert.Equal(2, actual.Dimension.NumRows);
            Assert.Equal(2, actual.Dimension.NumCols);
            Assert.Equal(1, actual.NumberOfMines);
        }

        [Fact]
        public void GetSquareFromCoordinate_InputCoordinate_ReturnSquare()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var expected = new Field(dimensions, 1, null);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            var actual = field.GetSquareFromCoordinate(coordinate);

            Assert.True(actual.HasMine());
        }

        [Fact]
        public void SetSquareToShowUsingCoordinate_InputCoordinate_ValidateSquareCanBeShown()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var expected = new Field(dimensions, 1, null);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            field.SetSquareToShowWithCoordinate(coordinate);

            var actual = field.GetSquareFromCoordinate(coordinate);

            Assert.True(actual.CanShow);
        }
    }
}
