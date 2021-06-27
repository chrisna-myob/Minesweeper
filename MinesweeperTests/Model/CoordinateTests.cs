using System;
using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class CoordinateTests
    {
        [Fact]
        public void Coordinate_InputIntegerXAndIntegerY_VerifyCoordinateIsCorrect()
        {
            var actual = new Coordinate(0, 0);

            Assert.Equal(0, actual.X);
            Assert.Equal(0, actual.Y);
        }
    }
}
