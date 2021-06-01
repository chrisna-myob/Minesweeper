using System;
using System.Collections.Generic;
using Minesweeper;
using Minesweeper.Repository;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class RulesTests
    {
        [Fact]
        public void HasWon_InputFieldService_ReturnFalse()
        {
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(rng => rng.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(0);
           var builder = new FieldBuilder(rng.Object);
           var field = builder.CreateField("EASY", new Dimension(1, 2));
           var fieldRepo = new FieldRepository(field);
           var fieldService = new FieldService(fieldRepo);

           var actual = Rules.HasWon(fieldService);

           Assert.False(actual);
        }

        [Fact]
        public void HasWon_InputFieldService_ReturnTrue()
        {
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(rng => rng.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(0)
               .Returns(0);
           var builder = new FieldBuilder(rng.Object);
           var field = builder.CreateField("EASY", new Dimension(1, 2));
           var fieldRepo = new FieldRepository(field);
           var fieldService = new FieldService(fieldRepo);

           fieldRepo.SetSquareToShow(new Coordinate(0, 1));

           var actual = Rules.HasWon(fieldService);

           Assert.True(actual);
        }

        [Fact]
        public void GameHasEnded_InputFieldService_ReturnFalse()
        {
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(rng => rng.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(0)
               .Returns(0);
           var builder = new FieldBuilder(rng.Object);
           var field = builder.CreateField("EASY", new Dimension(1, 2));
           var fieldRepo = new FieldRepository(field);
           var fieldService = new FieldService(fieldRepo);

           var actual = Rules.GameHasEnded(fieldService);

           Assert.False(actual);
        }

        [Fact]
        public void GameHasEnded_InputFieldService_ReturnTrue()
        {
           var rng = new Mock<INumberGenerator>();
           rng.SetupSequence(rng => rng.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(0)
               .Returns(0);
           var builder = new FieldBuilder(rng.Object);
           var field = builder.CreateField("EASY", new Dimension(1, 2));
           var fieldRepo = new FieldRepository(field);
           var fieldService = new FieldService(fieldRepo);
           fieldRepo.SetSquareToShow(new Coordinate(0, 1));

           var actual = Rules.GameHasEnded(fieldService);

           Assert.True(actual);
        }
    }
}
