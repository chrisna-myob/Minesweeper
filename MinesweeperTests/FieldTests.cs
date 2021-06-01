using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class FieldTests
    {
        private readonly Dimension dimension;
        private readonly Coordinate coordinate;
        private readonly Mock<INumberGenerator> rng;
        private readonly FieldBuilder builder;
        
        public FieldTests()
        {
           dimension = new Dimension(2, 2);
           coordinate = new Coordinate(0, 0);
           rng = new Mock<INumberGenerator>();
           rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(0)
               .Returns(0);
           builder = new FieldBuilder(rng.Object);
        }

        [Fact]
        public void Field_InputRowsColumnsListOfMineCoordinatesAndNumberOfMines_ReturnFieldObject()
        {
           var expected = new Field(dimension, 1, null, null);

           var actual = builder.CreateField("EASY", dimension);

           Assert.Equal(2, actual.Dimension.NumRows);
           Assert.Equal(2, actual.Dimension.NumCols);
        }

        [Fact]
        public void SetSquareToShowWithCoordinate_InputCoordinate_ValidateSquareCanBeShown()
        {
           var expected = new Field(dimension, 1, null, null);
           var field = builder.CreateField("EASY", dimension);
           field.SetSquareToShowWithCoordinate(coordinate);

           var actual = field.CanShowSquare(coordinate);

           Assert.True(actual);
        }

        [Fact]
        public void CanShowSquare_InputCoordinate_ReturnTrue()
        {
           var field = builder.CreateField("EASY", dimension);
           field.SetSquareToShowWithCoordinate(coordinate);

           var actual = field.CanShowSquare(coordinate);

           Assert.True(actual);
        }

        [Fact]
        public void CanShowSquare_InputCoordinate_ReturnFalse()
        {
           var field = builder.CreateField("EASY", dimension);

           var actual = field.CanShowSquare(coordinate);

           Assert.False(actual);
        }

        [Fact]
        public void CoordinateInFieldHasHintLargerThanZero_InputCoordinate_ReturnTrue()
        {
           var coord = new Coordinate(0, 1);
           var field = builder.CreateField("EASY", dimension);

           var actual = field.CoordinateHasHintLargerThanZero(coord);

           Assert.True(actual);
        }

        [Fact]
        public void CoordinateInFieldHasHintLargerThanZero_InputCoordinate_ReturnFalse()
        {
           var dimensions = new Dimension(3, 3);
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(2)
               .Returns(2);

           var builder = new FieldBuilder(rng.Object);
           var field = builder.CreateField("EASY", dimensions);

           var actual = field.CoordinateHasHintLargerThanZero(coordinate);

           Assert.False(actual);
        }
    }
}
