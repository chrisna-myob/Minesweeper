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
    }
}
