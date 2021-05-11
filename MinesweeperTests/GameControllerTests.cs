using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class GameControllerTests
    {
        private readonly Mock<INumberGenerator> rng;
        private readonly Mock<IIO> io;

        public GameControllerTests()
        {
            rng = new Mock<INumberGenerator>();
            io = new Mock<IIO>();
        }

        //[Fact]
        //public void GameResult_InputField_ReturnLose()
        //{
        //    var console = new Mock<IIO>();
        //    var rng = new Mock<INumberGenerator>();
        //    var coordinate = new Coordinate(0, 0);
        //    var dimensions = new Dimension(2, 2);
        //    rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
        //        .Returns(1)
        //        .Returns(0)
        //        .Returns(0);
        //    var builder = new FieldBuilder(rng.Object);
        //    var gameController = new GameController(console.Object, builder);
        //    var field = builder.CreateField(dimensions);

        //    var actual = gameController.GameResult(field);

        //    Assert.Equal(GameResult.LOSE, actual);
        //    io.Verify(x => x.WriteLine("Welcome to Minesweeper!"), Times.Once);
        //}

        [Fact]
        public void PrintWelcomeMessage_VerifyMessageIsCalled()
        {
            var builder = new Mock<IBuild>();
            var gameController = new GameController(io.Object, builder.Object);
            gameController.PrintWelcomeMessage();

            io.Verify(x => x.WriteLine("Welcome to Minesweeper!"), Times.Once);
        }

        //[Fact]
        //public void GetValidDimensionInput_InputStringDimension_ReturnStringDimension()
        //{
        //    var expected = "2,2";
        //    io.SetupSequence(i => i.ReadLine())
        //        .Returns(expected);
        //    var builder = new Mock<IBuild>();
        //    var gameController = new GameController(io.Object, builder.Object);

        //    var actual = gameController.GetValidDimensionInput();

        //    Assert.Equal(expected, actual);
        //}
    }
}
