using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class RulesTests
    {
        private readonly Dimension dimension;
        private readonly Coordinate coordinate;
        private readonly Mock<INumberGenerator> rng;
        private readonly FieldBuilder builder;

        public RulesTests()
        {
            dimension = new Dimension(2, 2);
            coordinate = new Coordinate(0, 0);
            rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            builder = new FieldBuilder(rng.Object);
        }

        [Fact]
        public void HasWon_InputField_ReturnTrue()
        {
            var field = builder.CreateField(dimension);
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(0, 1));
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(1, 0));
            field.SetAdjacentCoordinatesInFieldToShow(new Coordinate(1, 1));

            var actual = Rules.HasWon(field);

            Assert.True(actual);
        }

        [Fact]
        public void HasWon_InputField_ReturnFalse()
        {
            var field = builder.CreateField(dimension);

            var actual = Rules.HasWon(field);

            Assert.False(actual);
        }

        [Fact]
        public void HasNotPreviouslyInputtedCoordinate_InputFieldAndCoordinate_ThrowInvalidInputException()
        {
            var field = builder.CreateField(dimension);
            field.SetAdjacentCoordinatesInFieldToShow(coordinate);

            var exception = Assert.Throws<InvalidInputException>(() => Rules.ValidateCoordinate(field, coordinate));

            Assert.Equal("You have already entered this coordinate.", exception.Message);
        }

        [Fact]
        public void CanShowIndividualCoordinateInField_InputFieldAndCoordinate_ReturnTrue()
        {
            var field = builder.CreateField(dimension);

            var actual = Rules.CanShowIndividualCoordinateInField(field, new Coordinate(0,1));

            Assert.True(actual);
        }

        [Fact]
        public void CanShowIndividualCoordinateInField_InputFieldAndCoordinate_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var dimension = new Dimension(3, 3);
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(2)
                .Returns(2);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(dimension);
            field.SetAdjacentCoordinatesInFieldToShow(coordinate);

            var actual = Rules.CanShowIndividualCoordinateInField(field, coordinate);

            Assert.False(actual);
        }

        [Theory]
        [MemberData(nameof(TestDataGenerator.GetCoordinatesToSetSquaresForShowing), MemberType = typeof(TestDataGenerator))]
        public void GameHasEnded_InputField_ReturnTrue(List<Coordinate> coordinates)
        {
            var field = builder.CreateField(dimension);

            foreach(var coord in coordinates)
            {
                field.SetAdjacentCoordinatesInFieldToShow(coord);
            }

            var actual = Rules.GameHasEnded(field);

            Assert.True(actual);
        }

        [Fact]
        public void GameHasEnded_InputField_ReturnFalse()
        {
            var field = builder.CreateField(dimension);

            var actual = Rules.GameHasEnded(field);

            Assert.False(actual);
        }
    }
}
