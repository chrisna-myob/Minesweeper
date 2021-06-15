using Minesweeper;
using Xunit;
using Moq;
using System.Collections.Generic;
using Minesweeper.Service;

namespace MinesweeperTests
{
    public class CoordinateServiceTests
    {
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetAdjacentCoordinates), MemberType = typeof(TestDataGenerator))]
        public void GetAdjacentCoordinates_InputCoordinate_ReturnListOfValidAdjacentCoordinates(Coordinate coordinate, List<Coordinate> expected)
        {
            var dimension = new Dimension(3, 3);
            var coordinateService = new CoordinateService();

            var actual = coordinateService.GetAdjacentCoordinates(coordinate, dimension);

            Assert.Equal(expected, actual);
        }
    }
}
