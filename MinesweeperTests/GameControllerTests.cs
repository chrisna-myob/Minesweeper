using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class GameControllerTests
    {
        private readonly Dimension dimension;
        private readonly Coordinate coordinate;
        private readonly Mock<INumberGenerator> rng;
        private readonly FieldBuilder builder;

        [Fact]
        public void Play_InputQ_ReturnQuit()
        {
            var io = new Mock<IIO>();
            io.Setup(input => input.ReadLine())
                .Returns("q");
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var gameController = new GameController(io.Object, builder);
            var actual = gameController.Play();

            Assert.Equal(GameStatus.QUIT, actual);
        }

        //[Fact]
        //public void Play_ReturnWin()
        //{
        //    var io = new Mock<IIO>();
        //    io.Setup(input => input.ReadLine())
        //        .Returns("q");
        //    var rng = new RandomNumberGenerator();
        //    var builder = new FieldBuilder(rng);
        //    var gameController = new GameController(io.Object, builder);
        //    var actual = gameController.Play();

        //    Assert.Equal(GameResult.QUIT, actual);
        //}

        [Fact]
        public void Play_ReturnLose()
        {
            var io = new Mock<IIO>();
            io.SetupSequence(input => input.ReadLine())
                .Returns("2,2")
                .Returns("1,1");

            var rng = new Mock<INumberGenerator>();

            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(1)
               .Returns(0)
               .Returns(0);

            var builder = new FieldBuilder(rng.Object);

            var gameController = new GameController(io.Object, builder);
            gameController.SetUpGame();
            var actual = gameController.Play();

            Assert.Equal(GameStatus.LOSE, actual);
        }

        [Fact]
        public void Play_ReturnWin()
        {
            var io = new Mock<IIO>();
            io.SetupSequence(input => input.ReadLine())
                .Returns("1,2")
                .Returns("1,2");

            var rng = new Mock<INumberGenerator>();

            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(1)
               .Returns(0)
               .Returns(0);

            var builder = new FieldBuilder(rng.Object);

            var gameController = new GameController(io.Object, builder);
            gameController.SetUpGame();
            var actual = gameController.Play();

            Assert.Equal(GameStatus.WIN, actual);
        }

        [Fact]
        public void Play_Verify()
        {
            var io = new Mock<IIO>();
            io.SetupSequence(input => input.ReadLine())
                .Returns("1,2")
                .Returns("1,2");

            var rng = new Mock<INumberGenerator>();

            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
               .Returns(1)
               .Returns(0)
               .Returns(0);

            var builder = new FieldBuilder(rng.Object);

            var gameController = new GameController(io.Object, builder);
            gameController.SetUpGame();
            var actual = gameController.Play();

            Assert.Equal(GameStatus.WIN, actual);
        }
    }
}
