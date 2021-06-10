using Minesweeper;
using Xunit;

namespace MinesweeperTests
{
    public class CoordinateFactoryTests
    {
        private readonly CoordinateFactory coordinateFactory;
        private readonly Validation validation;

        public CoordinateFactoryTests()
        {
            coordinateFactory = new CoordinateFactory();
            validation = new Validation();
        }

        [Fact]
        public void MakeCoordinate_InputValidStringAndDimension_ReturnCoordinate()
        {
            var expected = new Coordinate(0, 0);

            var actual = coordinateFactory.MakeCoordinate(new Dimension(2, 2), "1,1", validation);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeCoordinate_InputInvalidStringAndDimension_ThrowInvalidInputException()
        {
            var actual = Assert.Throws<InvalidInputException>(() => coordinateFactory.MakeCoordinate(new Dimension(2, 2), "-1,1", validation));

            Assert.Equal("Coordinate cannot be negative\n", actual.Message);
        }
    }
}
