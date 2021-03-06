using System;
using Minesweeper;
using Xunit;
using Moq;
using System.Collections.Generic;

namespace MineTrialTests
{
    public class ValidationTests
    {
        private readonly Validation validation;
        private readonly Dimension _dimension;

        public ValidationTests()
        {
            validation = new Validation();
            _dimension = new Dimension(3, 3);
        }

        [Theory]
        [InlineData("-1,2")]
        [InlineData("1,-2")]
        [InlineData("-1,-2")]
        public void IsFieldDimensionInputValid_InputNegativeDimension_ThrowInvalidInputException(string dimension)
        {
            var exception = Assert.Throws<InvalidInputException>(() => validation.IsFieldDimensionInputValid(dimension));

            Assert.Equal("Dimension cannot be negative\n", exception.Message);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("1212")]
        [InlineData("a,b")]
        public void IsFieldDimensionInputValid_InputDimensionWithIncorrectFormat_ThrowInvalidInputException(string dimension)
        {
            var exception = Assert.Throws<InvalidInputException>(() => validation.IsFieldDimensionInputValid(dimension));

            Assert.Equal("Dimension must be in the format row,column with integer values\n", exception.Message);
        }

        [Theory]
        [InlineData("0,0")]
        [InlineData("8,0")]
        public void IsFieldDimensionInputValid_InputDimensionWithZero_ThrowInvalidInputException(string dimension)
        {
            var exception = Assert.Throws<InvalidInputException>(() => validation.IsFieldDimensionInputValid(dimension));

            Assert.Equal("Dimension values must be between 1 - 100\n", exception.Message);
        }

        [Theory]
        [InlineData("-1,-1")]
        public void IsCoordinateInputValid_InputNegativeCoordinate_ThrowInvalidInputException(string coordinate)
        {
            var exception = Assert.Throws<InvalidInputException>(() => validation.IsCoordinateInputValid(_dimension, coordinate));

            Assert.Equal("Coordinate cannot be negative\n", exception.Message);
        }

        [Theory]
        [InlineData("4,5")]
        [InlineData("0,0")]
        [InlineData("0,1")]
        [InlineData("1,0")]
        public void IsCoordinateInputValid_InputCoordinateSmallerOrLargerThanDimensions_ThrowInvalidInputException(string coordinate)
        {
            var exception = Assert.Throws<InvalidInputException>(() => validation.IsCoordinateInputValid(_dimension, coordinate));

            Assert.Equal("Coordinate must be within the field bounds\n", exception.Message);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("12")]
        [InlineData("a,b")]
        public void IsCoordinateInputValid_InputCoordinateWithIncorrectFormat_ThrowInvalidInputException(string coordinate)
        {
            var exception = Assert.Throws<InvalidInputException>(() => validation.IsCoordinateInputValid(_dimension, coordinate));

            Assert.Equal("Coordinate must be in the format row,column with integer values\n", exception.Message);
        }

        [Fact]
        public void CoordinateHasAlreadyBeenUncovered_InputTrue_ThrowInvalidInputException()
        {
            var actual = Assert.Throws<InvalidInputException>(() => validation.CoordinateHasAlreadyBeenUncovered(true));

            Assert.Equal("You have already entered this coordinate.\n", actual.Message);
        }
    }
}
