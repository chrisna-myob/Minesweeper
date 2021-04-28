using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class FieldBuilderTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetWinningCoordinatesFromDataGenerator), MemberType = typeof(TestDataGenerator))]
        public void MakeUniqueMineCoordinates_InputNumberOfMinesIntegerCoordinatesRowsAndColumns_ReturnListOfUniqueCoordinates(int numberOfMines, Queue<int> randomNumbers, int rows, int columns, List<Coordinate> expected)
        {
            var rng = new Mock<INumberGenerator>();

            rng.Setup(i => i.GetRandomNumber(0, columns))
               .Returns(randomNumbers.Dequeue);


            var actual = FieldBuilder.MakeUniqueMineCoordinates(rng.Object, numberOfMines, rows, columns);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeField_InputRNGOneMineAndDimensionsReturnField()
        {
            var rng = new Mock<INumberGenerator>();
            var numberOfMines = 1;
            rng.Setup(i => i.GetRandomNumber(0, 1))
               .Returns(new Queue<int>(new[] { 0, 0 }).Dequeue);
            var mineCoordinates = FieldBuilder.MakeUniqueMineCoordinates(rng.Object, numberOfMines, 1, 2);

            var actual = FieldBuilder.MakeField(1, 2, mineCoordinates, numberOfMines);

            Assert.True(actual[0, 0].HasMine());
            Assert.False(actual[0, 1].HasMine());
        }

        [Fact]
        public void CalculateHints_InputSmallFieldWithOneMine()
        {
            var numberOfMines = 1;

            var field = FieldBuilder.MakeField(2, 2, new List<Coordinate> { new Coordinate(0, 0) }, numberOfMines);
            FieldBuilder.CalculateHints(field, 2, 2);

            Assert.Equal("1", field[0, 1].RevealSquare());
            Assert.Equal("1", field[1, 0].RevealSquare());
            Assert.Equal("1", field[1, 1].RevealSquare());

        }

        [Fact]
        public void CalculateHints_InputMediumFieldWithTwoMines()
        {
            var numberOfMines = 2;

            var field = FieldBuilder.MakeField(3, 3, new List<Coordinate> { new Coordinate(0, 2), new Coordinate(1,1) }, numberOfMines);
            FieldBuilder.CalculateHints(field, 3, 3);

            Assert.Equal("1", field[0, 0].RevealSquare());
            Assert.Equal("2", field[0, 1].RevealSquare());
            Assert.Equal("1", field[1, 0].RevealSquare());
            Assert.Equal("2", field[1, 2].RevealSquare());
            Assert.Equal("1", field[2, 0].RevealSquare());
            Assert.Equal("1", field[2, 1].RevealSquare());
            Assert.Equal("1", field[2, 2].RevealSquare());

        }

        [Fact]
        public void CalculateHints_InputMediumFieldWithThreeMines()
        {
            var numberOfMines = 3;

            var field = FieldBuilder.MakeField(3, 3, new List<Coordinate> { new Coordinate(0, 2), new Coordinate(1, 1), new Coordinate(2,2) }, numberOfMines);
            FieldBuilder.CalculateHints(field, 3, 3);

            Assert.Equal("1", field[0, 0].RevealSquare());
            Assert.Equal("2", field[0, 1].RevealSquare());
            Assert.Equal("1", field[1, 0].RevealSquare());
            Assert.Equal("3", field[1, 2].RevealSquare());
            Assert.Equal("1", field[2, 0].RevealSquare());
            Assert.Equal("2", field[2, 1].RevealSquare());

        }
    }
}
