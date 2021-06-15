﻿using System;
using Xunit;
using Moq;
using Minesweeper;

namespace MinesweeperTests
{
    public class MineCoordinateFactoryTests
    {
        private readonly Mock<INumberGenerator> rng;

        public MineCoordinateFactoryTests()
        {
            rng = new Mock<INumberGenerator>();
        }

        [Fact]
        public void MakeUniqueMineCoordinates_InputEasyDifficultyAndDimension_VerifyReturnOfCoordinateListWithCountOf1()
        {
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.EASY, new Dimension(3, 3));

            Assert.Single(actual);
        }

        [Fact]
        public void MakeUniqueMineCoordinates_InputEasyDifficultyAndDimension_VerifyReturnOfCoordinateListWithCount()
        {
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0)
                .Returns(0)
                .Returns(1);
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.EASY, new Dimension(5, 5));

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
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.INTERMEDIATE, new Dimension(5, 5));

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
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);

            var actual = mineCoordinateFactory.MakeUniqueMineCoordinates(DifficultyLevel.EXPERT, new Dimension(5, 5));

            Assert.Equal(4, actual.Count);
        }
    }
}