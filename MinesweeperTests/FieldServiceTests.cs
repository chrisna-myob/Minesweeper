using System.Collections.Generic;
using Xunit;
using Moq;
using Minesweeper;
using Minesweeper.Repository;

namespace MinesweeperTests
{
    public class FieldServiceTests
    {
        private readonly IFieldRepository _fieldRepo;

        public FieldServiceTests()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(2, 2));
            _fieldRepo = new FieldRepository(field);
        }

        [Fact]
        public void GetDimension_ReturnsDimension()
        {
            var expected = new Dimension(2,2);
            var fieldService = new FieldService(_fieldRepo);
            
            var actual = fieldService.GetDimension();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void HandleCoordinate_InputCoordinate_VerifySquareWithInputtedCoordinateCanBeShown()
        {
            var fieldService = new FieldService(_fieldRepo);
            var coordinate = new Coordinate(0, 1);

            fieldService.HandleCoordinate(coordinate);
            var actual = _fieldRepo.CanShowSquare(coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void HasWon_InputCoordinatesThatResultInWin_ReturnTrue()
        {
            var fieldService = new FieldService(_fieldRepo);
            var coordinate = new Coordinate(0, 1);
            var coordinate2 = new Coordinate(1, 0);
            var coordinate3 = new Coordinate(1, 1);
            fieldService.HandleCoordinate(coordinate);
            fieldService.HandleCoordinate(coordinate2);
            fieldService.HandleCoordinate(coordinate3);

            var actual = _fieldRepo.CanShowSquare(coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void HasWon_ReturnFalse()
        {
            var fieldService = new FieldService(_fieldRepo);

            var actual = fieldService.HasWon();

            Assert.False(actual);
        }

        [Fact]
        public void HasLost_ReturnFalse()
        {
            var fieldService = new FieldService(_fieldRepo);

            var actual = fieldService.HasLost();

            Assert.False(actual);
        }

        [Fact]
        public void HasLost_InputCoordinateCorrespondingToAMine_ReturnTrue()
        {
            var fieldService = new FieldService(_fieldRepo);
            var coordinate = new Coordinate(0, 0);
            fieldService.HandleCoordinate(coordinate);

            var actual = fieldService.HasLost();

            Assert.True(actual);
        }

        [Fact]
        public void CoordinateHasAlreadyBeenUsed_InputCoordinateThatCanAlreadyBeShown_ThrowInvalidInputException() {
            var fieldService = new FieldService(_fieldRepo);
            var coordinate = new Coordinate(0, 1);
            fieldService.HandleCoordinate(coordinate);

            var exception = Assert.Throws<InvalidInputException>(() => fieldService.CoordinateHasAlreadyBeenUsed(coordinate));
            Assert.Equal("You have already entered this coordinate.\n", exception.Message);
        }

        [Fact]
        public void UncoveredBoardToString_ReturnString() {
            var expected = " ------- \n| * | 1 |\n ------- \n| 1 | 1 |\n ------- \n\n";
            var fieldService = new FieldService(_fieldRepo);

            var actual = fieldService.UncoveredBoardToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BoardToString_ReturnString() {
            var expected = " ------- \n| . | . |\n ------- \n| . | . |\n ------- \n\n";
            var fieldService = new FieldService(_fieldRepo);

            var actual = fieldService.BoardToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BoardToString_InputOneCoordinate_ReturnString() {
            var expected = " ------- \n| . | 1 |\n ------- \n| . | . |\n ------- \n\n";
            var fieldService = new FieldService(_fieldRepo);
            var coordinate = new Coordinate(0, 1);
            fieldService.HandleCoordinate(coordinate);

            var actual = fieldService.BoardToString();

            Assert.Equal(expected, actual);
        }
    }
}
