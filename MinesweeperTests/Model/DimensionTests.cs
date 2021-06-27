using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class DimensionTests
    {
        [Fact]
        public void Dimension_InputRowAndColumnIntegers_VerifyEqualDimension()
        {
            var row = 4;
            var column = 5;
            var expected = new Dimension(row, column);

            var actual = new Dimension(row, column);

            Assert.Equal(expected, actual);
        }
    }
}
