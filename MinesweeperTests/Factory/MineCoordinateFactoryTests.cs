using System;
using Xunit;
using Moq;
using Minesweeper;

namespace MinesweeperTests
{
    public class MineCoordinateFactoryTests
    {
        private readonly Mock<INumberGenerator> rng;
        private readonly Dimension _dimension;
        private readonly MineCoordinateFactory mineCoordinateFactory;

        public MineCoordinateFactoryTests()
        {
            rng = new Mock<INumberGenerator>();
            _dimension = new Dimension(5, 5);
            mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
        }

        [Fact]
        public void MakeUniqueMineCoordinates_InputEasyDifficultyAndDimension_VerifyReturnOfCoordinateListWithCountOf2()
        {
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.EASY, _dimension);

            Assert.Equal(2, actual.Count);
        }

        [Fact]
        public void MakeUniqueMineCoordinates_InputIntermediateDifficultyAndDimension_ReturnValidCoordinateList()
        {
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0)
                .Returns(2);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.INTERMEDIATE, _dimension);

            Assert.Equal(3, actual.Count);
        }

        [Fact]
        public void MakeUniqueMineCoordinates_InputExpertDifficultyAndDimension_ReturnValidCoordinateList()
        {
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1)
                .Returns(0)
                .Returns(2)
                .Returns(0)
                .Returns(3);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.EXPERT, _dimension);

            Assert.Equal(4, actual.Count);
        }
    }
}
