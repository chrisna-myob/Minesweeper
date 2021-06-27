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
        private readonly Mock<INumberGenerator> _rng;
        private readonly GameService _gameService;
        private readonly Field _field;
        private readonly FieldService _fieldService;

        public GameServiceTests()
        {
            _rng = new Mock<INumberGenerator>();
            var coordinateService = new CoordinateService();
            _fieldService = new FieldService(coordinateService);
            var dimensionFactory = new DimensionFactory();
            var coordinateFactory = new CoordinateFactory();
            var validation = new Validation();
            var mineCoordinateFactory = new MineCoordinateFactory(_rng.Object);
            var gridFactory = new GridFactory(coordinateService);
            _gameService = new GameService(_fieldService, dimensionFactory, coordinateFactory, validation, mineCoordinateFactory, gridFactory);

            var mineCoordinates = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = new ISquare[,] { { new MineSquare(), new SafeSquare() }, { new SafeSquare(), new SafeSquare() } };
            _field = new Field(new Dimension(2, 2), mineCoordinates, grid);
        }

        [Fact]
        public void GameRound_InputQ_ReturnQuitState()
        {
            var actual = _gameService.GameRound("q");

            Assert.Equal(GameState.QUIT, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnPlayState()
        {
            _rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            _gameService.SetUpField("EASY", "2,2");

            var actual = _gameService.GameRound("1,2");

            Assert.Equal(GameState.PLAY, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnWinState()
        {
            _rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            _gameService.SetUpField("EASY", "2,2");
            _gameService.GameRound("2,1");
            _gameService.GameRound("2,2");

            var actual = _gameService.GameRound("1,2");

            Assert.Equal(GameState.WIN, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnLoseState()
        {
            _rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(0)
                .Returns(0);
            _gameService.SetUpField("EASY", "2,2");

            var actual = _gameService.GameRound("1,1");

            Assert.Equal(GameState.LOSE, actual);
        }

        [Fact]
        public void GetGrid_InputGameStatePLAY_ReturnStringOfGrid()
        {
            _fieldService.SetField(_field);
            var expected = " ------- \n| . | . |\n ------- \n| . | . |\n ------- \n\n";

            var actual = _gameService.GetGrid(GameState.PLAY);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetGrid_InputGameStateADMIN_ReturnStringOfGrid()
        {
            _gameService.SetUpField("EASY", "2,2");
            var expected = " ------- \n| * | 1 |\n ------- \n| 1 | 1 |\n ------- \n\n";

            var actual = _gameService.GetGrid(GameState.ADMIN);

            Assert.Equal(expected, actual);
        }

    }
}

