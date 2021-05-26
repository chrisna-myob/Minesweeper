using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;
using Minesweeper.Repository.Interfaces;
using Minesweeper.Model;

namespace MinesweeperTests
{
    public class EndToEndTests
    {
        private readonly Mock<INumberGenerator> rng;

        public EndToEndTests()
        {
            rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
        }

        [Fact]
        public void GameEndsWithPlayerWinning()
        {
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            input.SetupSequence(i => i.GetUserInput())
                .Returns("2,2")
                .Returns("1,2")
                .Returns("2,1")
                .Returns("2,2");
            var coordinate = new CoordinateRepository();
            var dimensionRepo = new DimensionRepository();
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo, coordinate);
            var gameController = new ConsoleGameController(gameService);

            gameController.Run();

            output.Verify(x => x.WriteLine("You've won the game :)"), Times.Once);

        }

        [Fact]
        public void GameEndsWithPlayerQuit()
        {
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            input.SetupSequence(i => i.GetUserInput())
                .Returns("2,2")
                .Returns("q");
            var coordinate = new CoordinateRepository();
            var dimensionRepo = new DimensionRepository();
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo, coordinate);
            var gameController = new ConsoleGameController(gameService);

            gameController.Run();

            output.Verify(x => x.WriteLine("You have quit the game."), Times.Once);

        }

        [Fact]
        public void GameEndsWithPlayerLosing()
        {
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            input.SetupSequence(i => i.GetUserInput())
                .Returns("2,2")
                .Returns("1,1");
            var coordinate = new CoordinateRepository();
            var dimensionRepo = new DimensionRepository();
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo, coordinate);
            var gameController = new ConsoleGameController(gameService);

            gameController.Run();

            output.Verify(x => x.WriteLine("You've lost :("), Times.Once);

        }
    }
}