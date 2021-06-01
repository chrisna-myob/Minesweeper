using Minesweeper;
using Xunit;
using Moq;
using System.Collections.Generic;

namespace MinesweeperTests
{
    public class GlobalHelperTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetAdjacentCoordinates), MemberType = typeof(TestDataGenerator))]
        public void GetAdjacentCoordinates_InputCoordinate_ReturnListOfValidAdjacentCoordinates(int x, int y, List<Coordinate> expected)
        {
           var numberOfMines = 0;
           var dimension = new Dimension(3, 3);
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(numberOfMines);
           var builder = new FieldBuilder(rng.Object);
           var fieldBuild = builder.CreateField("EASY", dimension);

           var actual = GlobalHelpers.GetAdjacentCoordinates(x, y, dimension);

           Assert.Equal(expected, actual);
        }
    }
}
