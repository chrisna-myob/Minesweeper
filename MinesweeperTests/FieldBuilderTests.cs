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
        public void CreateField_InputDifficultyAndDimension_ReturnField()
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

        [Fact]
        public void CreateField_InputEasyDifficultyAndDimension_ReturnTrueForNumberOfMines()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EASY", new Dimension(5, 5));

            var actual = field.NumberOfMines;

            Assert.Equal(2, actual);
        }

        [Fact]
        public void CreateField_InputIntermediateDifficultyAndDimension_ReturnTrueForNumberOfMines()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0)
                .Returns(2);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("INTERMEDIATE", new Dimension(5, 5));

            var actual = field.NumberOfMines;

            Assert.Equal(3, actual);
        }

        [Fact]
        public void CreateField_InputExpertDifficultyAndDimension_ReturnTrueForNumberOfMines()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0)
                .Returns(2)
                .Returns(0)
                .Returns(3);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField("EXPERT", new Dimension(5, 5));

            var actual = field.NumberOfMines;

            Assert.Equal(4, actual);
        }
    }
}
