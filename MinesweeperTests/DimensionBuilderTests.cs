using Minesweeper;
using Xunit;
using Minesweeper.Model;

namespace MinesweeperTests
{
    public class DimensionBuilderTests
    {
        [Fact]
        public void MakeDimension_InputValidStringAndDimension_ReturnCoordinate()
        {
           var expected = new Dimension(1, 1);
           var dimensionRepo = new DimensionRepository();

           var actual = dimensionRepo.MakeDimension("1,1");

           Assert.Equal(expected, actual);
        }

        [Fact]
        public void MakeDimension_InputInvalidStringAndDimension_ThrowInvalidInputException()
        {
           var actual = Assert.Throws<InvalidInputException>(() => Validate.IsFieldDimensionInputValid("-1,1"));

           Assert.Equal("Dimension cannot be negative", actual.Message);
        }
    }
}
