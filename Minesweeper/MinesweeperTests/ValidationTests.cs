using System;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class ValidationTests
    {
        [Fact]
        public void IsFieldDimensionInputValid_InputValidStringDimensionOfField_ReturnTrue()
        {
            var actual = Validation.IsFieldDimensionInputValid("1,2");

            Assert.True(actual);
        }

        [Theory]
        [InlineData("-1,2")]
        [InlineData("1,-2")]
        [InlineData("-1,-2")]
        [InlineData("a,b")]
        [InlineData("ab")]
        [InlineData("1212")]
        [InlineData("0,0")]
        [InlineData("8,0")]
        public void IsFieldDimensionInputValid_InputInvalidStringDimensionOfField_ReturnFalse(string dimension)
        {
            var actual = Validation.IsFieldDimensionInputValid(dimension);

            Assert.False(actual);
        }

        [Fact]
        public void IsCoordinateInputValid_InputValidCoordinate_ReturnTrue()
        {
            var dimension = new Dimension(3, 3);
            var field = new Field(dimension, 1, null);

            var actual = Validation.IsCoordinateInputValid(dimension, "1,1");

            Assert.True(actual);
        }

        [Theory]
        [InlineData("-1,-1")]
        [InlineData("4,5")]
        [InlineData("ab")]
        [InlineData("a,b")]
        [InlineData("0,0")]
        [InlineData("0,1")]
        [InlineData("1,0")]
        public void IsCoordinateInputValid_InputInvalidCoordinate_ReturnFalse(string coordinate)
        {
            var dimension = new Dimension(3, 3);
            var field = new Field(dimension, 1, null);

            var actual = Validation.IsCoordinateInputValid(dimension, coordinate);

            Assert.False(actual);
        }

    }
}
