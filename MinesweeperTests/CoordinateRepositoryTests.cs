using Minesweeper;
using Minesweeper.Model;
using Xunit;

namespace MinesweeperTests
{
    public class CoordinateRepositoryTests
    {
        [Fact]
        public void MakeCoordinate_InputValidStringAndDimension_ReturnCoordinate()
        {
            var coordRepo = new CoordinateRepository();
            var expected = new Coordinate(0, 0);

            var actual = coordRepo.MakeCoordinate("1,1", new Dimension(2, 2));

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeCoordinate_InputInvalidStringAndDimension_ThrowInvalidInputException()
        {
            var actual = Assert.Throws<InvalidInputException>(() => Validation.IsCoordinateInputValid(new Dimension(2, 2), "-1,1"));

            Assert.Equal("Coordinate cannot be negative\n", actual.Message);
        }
    }
}
