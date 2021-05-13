using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class FieldBuilderTests
    {
        private readonly Mock<INumberGenerator> rng;

        public FieldBuilderTests()
        {
            rng = new Mock<INumberGenerator>();
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetWinningCoordinatesFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void MakeUniqueMineCoordinates_InputNumberOfMinesIntegerCoordinatesRowsAndColumns_ReturnListOfUniqueCoordinates(int numberOfMines, Queue<int> randomNumbers, Dimension dimension, List<Coordinate> expected)
        {
            rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(randomNumbers.Dequeue);
            var builder = new FieldBuilder(rng.Object);

            var actual = builder.MakeUniqueMineCoordinates(numberOfMines, dimension);

            Assert.Equal(expected, actual);
        }


        [Fact]
        public void MakeField_InputRNGOneMineAndDimensionsReturnField()
        {
            var numberOfMines = 1;
            var dimension = new Dimension(1, 2);
            rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(new Queue<int>(new[] { 0, 0 }).Dequeue);
            var builder = new FieldBuilder(rng.Object);
            var mineCoordinates = builder.MakeUniqueMineCoordinates(numberOfMines, dimension);

            var actual = builder.MakeBoard(dimension, mineCoordinates, numberOfMines);

            Assert.True(actual[0, 0].HasMine());
            Assert.False(actual[0, 1].HasMine());
        }

        [Fact]
        public void CalculateHints_InputFieldWithOneMine()
        {
            var numberOfMines = 1;
            var dimension = new Dimension(2, 2);
            var builder = new FieldBuilder(rng.Object);
            var mineCoordinates = builder.MakeUniqueMineCoordinates(numberOfMines, dimension);

            var field = builder.MakeBoard(dimension, new List<Coordinate> { new Coordinate(0, 0) }, numberOfMines);
            builder.CalculateHints(field, dimension);

            Assert.Equal("1", field[0, 1].GetSquareValue());
            Assert.Equal("1", field[1, 0].GetSquareValue());
            Assert.Equal("1", field[1, 1].GetSquareValue());

        }

        [Fact]
        public void CalculateHints_InputFieldWithTwoMines_ReturnStringOfCorrectHintValue()
        {
            var numberOfMines = 2;
            var dimension = new Dimension(3, 3);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.MakeBoard(dimension, new List<Coordinate> { new Coordinate(0, 2), new Coordinate(1, 1) }, numberOfMines);
            builder.CalculateHints(field, dimension);

            Assert.Equal("1", field[0, 0].GetSquareValue());
            Assert.Equal("2", field[0, 1].GetSquareValue());
            Assert.Equal("1", field[1, 0].GetSquareValue());
            Assert.Equal("2", field[1, 2].GetSquareValue());
            Assert.Equal("1", field[2, 0].GetSquareValue());
            Assert.Equal("1", field[2, 1].GetSquareValue());
            Assert.Equal("1", field[2, 2].GetSquareValue());

        }
    }
}
