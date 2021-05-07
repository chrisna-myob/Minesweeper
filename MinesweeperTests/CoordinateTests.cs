using System;
using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class CoordinateTests
    {
        [Fact]
        public void Coordinate_InputIntegerXAndIntegerY_ReturnCoordinate()
        {
            var coordinate = new Coordinate(0, 0);

            Assert.Equal(0, coordinate.X);
            Assert.Equal(0, coordinate.Y);
        }
    }
}
