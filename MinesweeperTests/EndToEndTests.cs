using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class EndToEndTests
    {
        //private readonly Mock<INumberGenerator> rng;
        //private readonly Mock<IIO> io;

        //public EndToEndTests()
        //{
        //    rng = new Mock<INumberGenerator>();
        //    io = new Mock<IIO>();
        //}

        //[Fact]
        //public void GameEndsWithPlayerWinning()
        //{
        //    io.SetupSequence(input => input.ReadLine())
        //        .Returns("3,3")
        //        .Returns("3,3")
        //        .Returns("1,3")
        //        .Returns("2,1")
        //        .Returns("1,1");

        //    rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
        //       .Returns(2)
        //       .Returns(0)
        //       .Returns(1)
        //       .Returns(2)
        //       .Returns(0);

        //    var builder = new FieldBuilder(rng.Object);

        //    var gameController = new GameController(io.Object, builder);
        //    gameController.Run();

        //    io.Verify(x => x.WriteLine("You've won the game :)"), Times.Once);

        //}

        //[Fact]
        //public void GameEndsWithPlayerLosing()
        //{
        //    io.SetupSequence(input => input.ReadLine())
        //        .Returns("3,3")
        //        .Returns("1,2");

        //    rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
        //       .Returns(2)
        //       .Returns(0)
        //       .Returns(1)
        //       .Returns(2)
        //       .Returns(0);

        //    var builder = new FieldBuilder(rng.Object);

        //    var gameController = new GameController(io.Object, builder);
        //    gameController.Run();

        //    io.Verify(x => x.WriteLine("You've lost :("), Times.Once);
        //}
    }
}