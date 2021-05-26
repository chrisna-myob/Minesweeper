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
            var expected = new Field(dimension, 1, null, null);

            var actual = builder.CreateField(dimension);

            Assert.Equal(2, actual.Dimension.NumRows);
            Assert.Equal(2, actual.Dimension.NumCols);
        }

        [Fact]
        public void SetSquareToShowWithCoordinate_InputCoordinate_ValidateSquareCanBeShown()
        {
            var expected = new Field(dimension, 1, null, null);
            var field = builder.CreateField(dimension);
            field.SetSquareToShowWithCoordinate(coordinate);

            var actual = field.CanShowSquare(coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void CanShowSquare_InputCoordinate_ReturnTrue()
        {
            var field = builder.CreateField(dimension);
            field.SetSquareToShowWithCoordinate(coordinate);

            var actual = field.CanShowSquare(coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void CanShowSquare_InputCoordinate_ReturnFalse()
        {
            var field = builder.CreateField(dimension);

            var actual = field.CanShowSquare(coordinate);

            Assert.False(actual);
        }

        [Fact]
        public void CoordinateInFieldHasHintLargerThanZero_InputCoordinate_ReturnTrue()
        {
            var coord = new Coordinate(0,1);
            var field = builder.CreateField(dimension);

            var actual = field.CoordinateHasHintLargerThanZero(coord);

            Assert.True(actual);
        }

        [Fact]
        public void CoordinateInFieldHasHintLargerThanZero_InputCoordinate_ReturnFalse()
        {
            var dimensions = new Dimension(3, 3);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(2)
                .Returns(2);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            var actual = field.CoordinateHasHintLargerThanZero(coordinate);

            Assert.False(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnTrue()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);
            field.SetSquareToShowWithCoordinate(new Coordinate(0, 1));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 0));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 1));

            var actual = field.RemainingSquaresAreMines();

            Assert.True(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(1)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            field.SetSquareToShowWithCoordinate(new Coordinate(0, 1));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 0));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 1));

            var actual = field.RemainingSquaresAreMines();

            Assert.False(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_InputField_ReturnTrue()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(2)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            field.SetSquareToShowWithCoordinate(new Coordinate(0, 0));

            var actual = field.MineHasBeenUncovered();

            Assert.True(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_InputField_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(2)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            var actual = field.MineHasBeenUncovered();

            Assert.False(actual);
        }

        [Fact]
        public void SetAdjacentCoordinatesInFieldToShow_InputCoordinateWithHintEqualToZero()
        {
            var coordinate = new Coordinate(0, 0);
            var dimension = new Dimension(3, 3);
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(1)
                .Returns(2);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimension);
            field.SetAdjacentCoordinatesInFieldToShow(coordinate);

            var actual = field.GetBoard();

            Assert.True(actual[0, 0].CanShow);
            Assert.True(actual[0, 1].CanShow);
            Assert.False(actual[0, 2].CanShow);
            Assert.True(actual[1, 0].CanShow);
            Assert.True(actual[1, 1].CanShow);
            Assert.True(actual[2, 0].CanShow);
            Assert.True(actual[2, 1].CanShow);
            Assert.False(actual[2, 2].CanShow);
            Assert.False(actual[1, 2].CanShow);
        }
    }
}
