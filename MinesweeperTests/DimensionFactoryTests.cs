using Minesweeper;
using Xunit;
using Minesweeper.Model;

namespace MinesweeperTests
{
    public class DimensionFactoryTests
    {
        private readonly DimensionFactory dimensionFactory;
        private readonly Validation validation;

        public DimensionFactoryTests()
        {
            dimensionFactory = new DimensionFactory();
            validation = new Validation();
        }

        [Fact]
        public void MakeDimension_InputValidStringAndDimension_ReturnCoordinate()
        {
           var expected = new Dimension(1, 1);

           var actual = dimensionFactory.MakeDimension("1,1", validation);

           Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeDimension_InputInvalidStringAndDimension_ThrowInvalidInputException()
        {
           var actual = Assert.Throws<InvalidInputException>(() => dimensionFactory.MakeDimension("-1,1", validation));

           Assert.Equal("Dimension cannot be negative\n", actual.Message);
        }
    }
}
