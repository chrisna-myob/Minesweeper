using System;
using Minesweeper;
using Xunit;
using Moq;

namespace MineTrialTests
{
    public class ValidationTests
    {
        private readonly Validation validation;

        public ValidationTests()
        {
            validation = new Validation();
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

           Assert.Equal("Dimension must be in the format x,y with integer values\n", exception.Message);
        }

        [Theory]
        [InlineData("0,0")]
        [InlineData("8,0")]
        public void IsFieldDimensionInputValid_InputDimensionWithZero_ThrowInvalidInputException(string dimension)
        {
           var exception = Assert.Throws<InvalidInputException>(() => validation.IsFieldDimensionInputValid(dimension));

           Assert.Equal("Dimension values must be larger than 0\n", exception.Message);
        }

        [Theory]
        [InlineData("-1,-1")]
        public void IsCoordinateInputValid_InputNegativeCoordinate_ThrowInvalidInputException(string coordinate)
        {
           var dimension = new Dimension(3, 3);
           var field = new Field(dimension, 1, null, null);

           var exception = Assert.Throws<InvalidInputException>(() => validation.IsCoordinateInputValid(dimension, coordinate));
           Assert.Equal("Coordinate cannot be negative\n", exception.Message);
        }

        [Theory]
        [InlineData("4,5")]
        [InlineData("0,0")]
        [InlineData("0,1")]
        [InlineData("1,0")]
        public void IsCoordinateInputValid_InputCoordinateSmallerOrLargerThanDimensions_ThrowInvalidInputException(string coordinate)
        {
           var dimension = new Dimension(3, 3);
           var field = new Field(dimension, 1, null, null);

           var exception = Assert.Throws<InvalidInputException>(() => validation.IsCoordinateInputValid(dimension, coordinate));
           Assert.Equal("Coordinate must be within the field bounds\n", exception.Message);
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("12")]
        [InlineData("a,b")]
        public void IsCoordinateInputValid_InputCoordinateWithIncorrectFormat_ThrowInvalidInputException(string coordinate)
        {
           var dimension = new Dimension(3, 3);
           var field = new Field(dimension, 1, null, null);

           var exception = Assert.Throws<InvalidInputException>(() => validation.IsCoordinateInputValid(dimension, coordinate));
           Assert.Equal("Coordinate must be in the format x,y with integer values\n", exception.Message);
        }

    }
}
