using System.Collections.Generic;
using Moq;
using Minesweeper;
using Xunit;
using Minesweeper.Factory;
using Minesweeper.Service;

namespace MinesweeperTests
{
    public class GameServiceTests
    {
        [Fact]
        public void GameRound_InputQ_ReturnQuitState()
        {
            var rng = new Mock<INumberGenerator>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);

            var actual = gameService.GameRound("q");

            Assert.Equal(GameState.QUIT, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnPlayState()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            gameService.SetUpField("EASY", "2,2");

            var actual = gameService.GameRound("1,2");

            Assert.Equal(GameState.PLAY, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnWinState()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            gameService.SetUpField("EASY", "2,2");
            gameService.GameRound("2,1");
            gameService.GameRound("2,2");

            var actual = gameService.GameRound("1,2");

            Assert.Equal(GameState.WIN, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnLoseState()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            gameService.SetUpField("EASY", "2,2");

            var actual = gameService.GameRound("1,1");

            Assert.Equal(GameState.LOSE, actual);
        }

        [Fact]
        public void GetBoard_InputGameStatePLAY_ReturnStringOfBoard()
        {
            var rng = new Mock<INumberGenerator>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            var grid = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };
            var field = new Field(new Dimension(2, 2), mineCoordinates, grid);
            fieldService.SetField(field);
            var expected = " ------- \n| . | . |\n ------- \n| . | . |\n ------- \n\n";

            var actual = gameService.GetBoard(GameState.PLAY);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetBoard_InputGameStateADMIN_ReturnStringOfBoard()
        {
            var rng = new Mock<INumberGenerator>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            var grid = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };
            var field = new Field(new Dimension(2, 2), mineCoordinates, grid);
            gameService.SetUpField("EASY", "2,2");
            var expected = " ------- \n| * | 1 |\n ------- \n| 1 | 1 |\n ------- \n\n";

            var actual = gameService.GetBoard(GameState.ADMIN);

            Assert.Equal(expected, actual);
        }

    }
}

