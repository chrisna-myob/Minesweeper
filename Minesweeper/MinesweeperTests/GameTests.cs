using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class GameTests
    {
        [Fact]
        public void GameResult_InputField_ReturnLose()
        {
            var console = new Mock<IIO>();
            var rng = new Mock<INumberGenerator>();
            var coordinate = new Coordinate(0, 0);
            var dimensions = new Dimension(2, 2);
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var gameController = new GameController(console.Object, builder);
            var field = builder.CreateField(dimensions);

            var actual = gameController.GameResult(field);

            Assert.Equal(GameResult.LOSE, actual);
        }
    }
}
