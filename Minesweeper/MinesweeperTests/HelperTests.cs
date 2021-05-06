using Minesweeper;
using Xunit;
using Moq;
using System.Collections.Generic;

namespace MinesweeperTests
{
    public class HelperTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetAdjacentCoordinates), MemberType = typeof(TestDataGenerator))]
        public void GetAdjacentCoordinates_InputCoordinate_ReturnListOfValidAdjacentCoordinates(int x, int y, List<Coordinate> expected)
        {
            var numberOfMines = 0;
            var dimension = new Dimension(3, 3);
            var rng = new Mock<INumberGenerator>();
            var builder = new FieldBuilder(rng.Object);
            var fieldBuild = builder.MakeField(dimension, null, numberOfMines);
            builder.CalculateHints(fieldBuild, dimension);
            var field = new Field(dimension, 0, fieldBuild);

            var actual = Helpers.GetAdjacentCoordinates(x, y, dimension);

            Assert.Equal(expected, actual);
        }
    }
}
