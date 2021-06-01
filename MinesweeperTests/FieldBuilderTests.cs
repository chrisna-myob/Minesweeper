using Minesweeper;
using Xunit;
using Minesweeper.Model;
using Moq;
using System.Collections.Generic;

namespace MinesweeperTests
{
    public class FieldBuilderTests
    {
        [Fact]
        public void MakeDimension_ReturnField()
        {
           var expected = new Field(new Dimension(2, 2), 1, new ISquare[,] { { new MineSquare(), new SafeSquare() } , { new SafeSquare(), new SafeSquare() } }, new List<Coordinate>() { new Coordinate(0, 0) });
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(0)
               .Returns(0);
           var builder = new FieldBuilder(rng.Object);

           var actual = builder.CreateField("EASY", new Dimension(2, 2));

           Assert.Equal(expected, actual);
        }
    }
}
