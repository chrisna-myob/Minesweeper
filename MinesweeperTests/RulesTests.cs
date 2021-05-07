using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class RulesTests
    {
        [Fact]
        public void CoordinateHasMineSquare_InputFieldAndCoordinate_ReturnExpectedBool()
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

            var actual = Rules.CoordinateHasMineSquare(field, coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void CoordinateHasMineSquare_InputFieldAndCoordinate_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 1);
            var dimensions = new Dimension(2, 2);
            var expected = new Field(dimensions, 1, null);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            var actual = Rules.CoordinateHasMineSquare(field, coordinate);

            Assert.False(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnTrue()
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
            field.SetSquareToShowWithCoordinate(new Coordinate(0, 1));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 0));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 1));

            var actual = Rules.RemainingSquaresAreMines(field);

            Assert.True(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var expected = new Field(dimensions, 1, null);
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

            var actual = Rules.RemainingSquaresAreMines(field);

            Assert.False(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnFalseAgain()
        {
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            var expected = new Field(dimensions, 1, null);
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(2)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0);

            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimensions);

            field.SetSquareToShowWithCoordinate(new Coordinate(0, 1));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 0));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 1));

            var actual = Rules.RemainingSquaresAreMines(field);

            Assert.False(actual);
        }

        [Fact]
        public void SetCoordinateToShow_InputFieldAndCoordinateWithHintLargerThanZero()
        {
            var numberOfMines = 1;
            var dimension = new Dimension(3, 3);
            var rng = new Mock<INumberGenerator>();
            var builder = new FieldBuilder(rng.Object);
            var fieldBuild = builder.MakeField(dimension, new List<Coordinate> { new Coordinate(0, 0) }, numberOfMines);
            builder.CalculateHints(fieldBuild, dimension);
            var field = new Field(dimension, 1, fieldBuild);
            Rules.SetCoordinatesToShow(field, new Coordinate(0, 1));

            var actual = field.GetSquareFromCoordinate(new Coordinate(0, 1));
            Assert.True(actual.CanShow);
        }

        [Fact]
        public void Sprawl_InputFieldAndCoordinateWithHintEqualToZero()
        {
            var coordinate = new Coordinate(0, 0);
            var numberOfMines = 1;
            var dimension = new Dimension(3, 3);
            var rng = new Mock<INumberGenerator>();
            var builder = new FieldBuilder(rng.Object);
            var fieldBuild = builder.MakeField(dimension, new List<Coordinate> { new Coordinate(1, 2) }, numberOfMines);
            builder.CalculateHints(fieldBuild, dimension);
            var field = new Field(dimension, 1, fieldBuild);
            Rules.SetCoordinatesToShow(field, coordinate);

            var actual = field.GetField();

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

        [Fact]
        public void Sprawl_InputFieldAndCoordinateWithHintEqualToZero_LargerMap()
        {
            var coordinate = new Coordinate(0, 0);
            var numberOfMines = 2;
            var dimension = new Dimension(4, 4);
            var rng = new Mock<INumberGenerator>();
            var builder = new FieldBuilder(rng.Object);
            var fieldBuild = builder.MakeField(dimension, new List<Coordinate> { new Coordinate(0, 3), new Coordinate(2, 1) }, numberOfMines);
            builder.CalculateHints(fieldBuild, dimension);
            var field = new Field(dimension, 1, fieldBuild);
            Rules.SetCoordinatesToShow(field, coordinate);

            var actual = field.GetField();

            Assert.True(actual[0, 0].CanShow);
            Assert.True(actual[0, 1].CanShow);
            Assert.True(actual[0, 2].CanShow);
            Assert.False(actual[0, 3].CanShow);
            Assert.True(actual[1, 0].CanShow);
            Assert.True(actual[1, 1].CanShow);
            Assert.True(actual[1, 2].CanShow);
            Assert.False(actual[1, 3].CanShow);
            Assert.False(actual[2, 0].CanShow);
            Assert.False(actual[2, 1].CanShow);
            Assert.False(actual[2, 2].CanShow);
            Assert.False(actual[2, 3].CanShow);
            Assert.False(actual[3, 0].CanShow);
            Assert.False(actual[3, 1].CanShow);
            Assert.False(actual[3, 2].CanShow);
            Assert.False(actual[3, 3].CanShow);
        }

        [Fact]
        public void MineHasBeenUncovered_InputField_ReturnTrue()
        {
            var coordinate = new Coordinate(0, 0);
            var numberOfMines = 1;
            var dimension = new Dimension(2, 2);
            var rng = new Mock<INumberGenerator>();
            var builder = new FieldBuilder(rng.Object);
            var fieldBuild = builder.MakeField(dimension, new List<Coordinate> { new Coordinate(0, 0) }, numberOfMines);
            builder.CalculateHints(fieldBuild, dimension);
            var field = new Field(dimension, 1, fieldBuild);
            Rules.SetCoordinatesToShow(field, coordinate);

            var actual = Rules.MineHasBeenUncovered(field);

            Assert.True(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_InputField_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var numberOfMines = 1;
            var dimension = new Dimension(2, 2);
            var rng = new Mock<INumberGenerator>();
            var builder = new FieldBuilder(rng.Object);
            var fieldBuild = builder.MakeField(dimension, new List<Coordinate> { new Coordinate(0, 0) }, numberOfMines);
            builder.CalculateHints(fieldBuild, dimension);
            var field = new Field(dimension, 1, fieldBuild);

            var actual = Rules.MineHasBeenUncovered(field);

            Assert.False(actual);
        }
    }
}
