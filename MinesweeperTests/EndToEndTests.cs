using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;
using Minesweeper.Factory;
using Minesweeper.Service;

namespace MinesweeperTests
{
    public class EndToEndTests
    {
        private readonly Mock<INumberGenerator> rng;

        public EndToEndTests()
        {
            rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
        }

        [Fact]
        public void GameEndsWithPlayerWinning()
        {
            var io = new Mock<IIO>();
            io.SetupSequence(i => i.GetUserInput())
                .Returns("EASY")
                .Returns("2,2")
                .Returns("1,2")
                .Returns("2,1")
                .Returns("2,2");
            var fieldService = new FieldService();
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var coordinateService = new CoordinateService();
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory, coordinateService);
            var gameController = new ConsoleGameController(gameService);

            gameController.Run();

            io.Verify(x => x.Write("You've won the game :)\n"), Times.Once);

        }

        [Fact]
        public void GameEndsWithPlayerQuit()
        {
            var io = new Mock<IIO>();
            io.SetupSequence(i => i.GetUserInput())
                .Returns("EASY")
                .Returns("2,2")
                .Returns("q");
            var fieldService = new FieldService();
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var coordinateService = new CoordinateService();
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory, coordinateService);
            var gameController = new ConsoleGameController(gameService);

            gameController.Run();

            io.Verify(x => x.Write("You have quit the game.\n"), Times.Once);
        }

        [Fact]
        public void GameEndsWithPlayerLosing()
        {
            var io = new Mock<IIO>();
            io.SetupSequence(i => i.GetUserInput())
                .Returns("EASY")
                .Returns("2,2")
                .Returns("1,1");
            var fieldService = new FieldService();
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var coordinateService = new CoordinateService();
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory, coordinateService);
            var gameController = new ConsoleGameController(gameService);

            gameController.Run();

            io.Verify(x => x.Write("You've lost :(\n"), Times.Once);
        }
    }
}