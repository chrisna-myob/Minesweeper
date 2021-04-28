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
        public void IsFieldDimensionInputValid_InputInvalidStringDimensionOfField_ReturnFalse(string dimension)
        {
            var actual = Validation.IsFieldDimensionInputValid(dimension);

            Assert.False(actual);
        }

        [Fact]
        public void IsCoordinateInputValid_InputValidCoordinate_ReturnTrue()
        {
            var field = new Field(3, 3, 1, null);
            var actual = Validation.IsCoordinateInputValid(field, "0,0");

            Assert.True(actual);
        }

        [Theory]
        [InlineData("-1,-1")]
        [InlineData("4,5")]
        [InlineData("ab")]
        [InlineData("a,b")]
        public void IsCoordinateInputValid_InputInvalidCoordinate_ReturnFalse(string coordinate)
        {
            var field = new Field(3, 3, 1, null);

            var actual = Validation.IsCoordinateInputValid(field, coordinate);

            Assert.False(actual);
        }

    }
}
