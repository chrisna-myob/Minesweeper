using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class FieldTests
    {
        private readonly Dimension dimension;
        private readonly Coordinate coordinate;
        private readonly Mock<INumberGenerator> rng;
        private readonly FieldBuilder builder;

        public FieldTests()
        {
            dimension = new Dimension(2,2);
            coordinate = new Coordinate(0, 0);
            rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            builder = new FieldBuilder(rng.Object);
        }

        [Fact]
        public void Field_InputRowsColumnsListOfMineCoordinatesAndNumberOfMines_ReturnFieldObject()
        {
            var expected = new Field(dimension, 1, null);

            var actual = builder.CreateField(dimension);

            Assert.Equal(2, actual.Dimension.NumRows);
            Assert.Equal(2, actual.Dimension.NumCols);
            Assert.Equal(1, actual.NumberOfMines);
        }

        [Fact]
        public void GetSquareFromCoordinate_InputCoordinate_ReturnSquare()
        {
            var expected = new Field(dimension, 1, null);
            var field = builder.CreateField(dimension);

            var actual = field.GetSquareFromCoordinate(coordinate);

            Assert.True(actual.HasMine());
        }

        [Fact]
        public void SetSquareToShowUsingCoordinate_InputCoordinate_ValidateSquareCanBeShown()
        {
            var expected = new Field(dimension, 1, null);
            var field = builder.CreateField(dimension);
            field.SetSquareToShowWithCoordinate(coordinate);

            var actual = field.GetSquareFromCoordinate(coordinate);

            Assert.True(actual.CanShow);
        }
    }
}
