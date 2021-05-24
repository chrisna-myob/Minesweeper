using System;
using Xunit;
using Minesweeper;

namespace MinesweeperTests
{
    public class DimensionTests
    {
        [Fact]
        public void Dimension_InputRowAndColumnIntegers()
        {
            var row = 4;
            var column = 5;

            var actual = new Dimension(4, 5);

            Assert.Equal(row, actual.NumRows);
            Assert.Equal(column, actual.NumCols);

        }
    }
}
