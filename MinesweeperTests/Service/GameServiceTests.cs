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
        [Theory]
        [InlineData("EASY", DifficultyLevel.EASY)]
        [InlineData("INTERMEDIATE", DifficultyLevel.INTERMEDIATE)]
        [InlineData("EXPERT", DifficultyLevel.EXPERT)]
        public void InitialiseField_VerifyFunctionIsAccessed(string input, DifficultyLevel expected)
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            var io = new Mock<IIO>();
            io.SetupSequence(i => i.GetUserInput())
                .Returns("EASY")
                .Returns("2,2")
                .Returns("1,2")
                .Returns("2,1")
                .Returns("2,2");
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            var actual = gameService.GetDifficulty(input);

            Assert.Equal(expected, actual);
        }

        [Theory]
        [InlineData("Hello", "Hello")]
        [InlineData(" Hello", "Hello")]
        [InlineData("Hello ", "Hello")]
        public void GetUserInput__InputString_ReturnString(string input, string expected)
        {
            var rng = new Mock<INumberGenerator>();
            var io = new Mock<IIO>();
            io.Setup(i => i.GetUserInput())
                .Returns(input);
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);

            var actual = gameService.GetTrimmedUserInput();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GameRound_InputQ_ReturnQuitState()
        {
            var rng = new Mock<INumberGenerator>();
            var io = new Mock<IIO>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);

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
            var io = new Mock<IIO>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            gameService.SetUpField(DifficultyLevel.EASY, "2,2");

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
            var io = new Mock<IIO>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            gameService.SetUpField(DifficultyLevel.EASY, "2,2");
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
            var io = new Mock<IIO>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            gameService.SetUpField(DifficultyLevel.EASY, "2,2");

            var actual = gameService.GameRound("1,1");

            Assert.Equal(GameState.LOSE, actual);
        }

        [Fact]
        public void DisplayMessage_InputQ_ReturnQuitState()
        {
            var rng = new Mock<INumberGenerator>();
            var io = new Mock<IIO>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);

            gameService.DisplayMessage("q");

            io.Verify(i => i.Write(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void DisplayBoard_InputQ_ReturnQuitState()
        {
            var rng = new Mock<INumberGenerator>();
            var io = new Mock<IIO>();
            var coordinateService = new CoordinateService();
            var fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(rng.Object);
            var mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            var gridFactory = new GridFactory(coordinateService);
            var gameService = new GameService(fieldService, io.Object, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);
            var grid = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };
            var field = new Field(new Dimension(2, 2), mineCoordinates, grid);
            fieldService.SetField(field);

            gameService.DisplayBoard(GameState.ADMIN);

            io.Verify(i => i.DisplayBoard(It.IsAny<string>()), Times.Once());
        }

    }
}

