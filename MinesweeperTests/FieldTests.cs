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
        public void SetSquareToShowWithCoordinate_InputCoordinate_ValidateCorrectSquaresCanBeShown()
        {
            var rng = new Mock<INumberGenerator>();
			rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(0)
				.Returns(0);
			var builder = new FieldBuilder(rng.Object);

           	var field = builder.CreateField("EASY", new Dimension(3, 3));

          	field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(1, 2));

			Assert.True(field.CanShowSquare(new Coordinate(0, 1)));
			Assert.True(field.CanShowSquare(new Coordinate(0, 2)));
			Assert.True(field.CanShowSquare(new Coordinate(1, 1)));
			Assert.True(field.CanShowSquare(new Coordinate(1, 2)));
			Assert.True(field.CanShowSquare(new Coordinate(2, 1)));
			Assert.True(field.CanShowSquare(new Coordinate(2, 2)));
        }

        [Fact]
        public void CanShowSquare_InputCoordinate_ReturnTrue()
        {
			var rng = new Mock<INumberGenerator>();
			rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(0)
				.Returns(0);
			var builder = new FieldBuilder(rng.Object);
			var field = builder.CreateField("EASY", new Dimension(2, 2));
			field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(0, 1));

			var actual = field.CanShowSquare(new Coordinate(0, 1));

			Assert.True(actual);
        }

        [Fact]
        public void CanShowSquare_InputCoordinate_ReturnFalse()
        {
           var rng = new Mock<INumberGenerator>();
			rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
				.Returns(0)
				.Returns(0);
			var builder = new FieldBuilder(rng.Object);
           	var field = builder.CreateField("EASY", new Dimension(2, 2));

			var actual = field.CanShowSquare(new Coordinate(0, 1));

			Assert.False(actual);
        }

        [Fact]
        public void ToString_ReturnString()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));
            var expected = " ------- \n| . | . |\n ------- \n| . | . |\n ------- \n\n";

            var actual = field.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UncoveredBoardToString_ReturnString()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));
            var expected = " ------- \n| * | 1 |\n ------- \n| 1 | 1 |\n ------- \n\n";

            var actual = field.UncoveredBoardToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_ReturnFalse()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));

            var actual = field.RemainingSquaresAreMines();

            Assert.False(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_ReturnTrue()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(0, 1));
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(1, 0));
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(1, 1));

            var actual = field.RemainingSquaresAreMines();

            Assert.True(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_ReturnFalse()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));

            var actual = field.MineHasBeenUncovered();

            Assert.False(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_ReturnTrue()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(0, 0));

            var actual = field.MineHasBeenUncovered();

            Assert.True(actual);
        }
    }
}
