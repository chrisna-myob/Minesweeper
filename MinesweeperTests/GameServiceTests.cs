using System.Collections.Generic;
using Moq;
using Minesweeper;
using Minesweeper.Model;
using Minesweeper.Repository.Interfaces;
using Xunit;
using Minesweeper.Repository;

namespace MinesweeperTests
{
    public class GameServiceTests
    {
        [Fact]
        public void InitialiseField_VerifyFunctionIsAccessed()
        {
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(3, 3));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);

            gameService.InitialiseField(It.IsAny<string>());

            dimensionRepo.Verify(func => func.MakeDimension(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void InitialiseField_InputInvalidString_ThrowInvalidInputException()
        {
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var dimension = new DimensionRepository();
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();

            var gameService = new GameService(input.Object, builder, output.Object, dimension, coordinate.Object);

            var actual = Assert.Throws<InvalidInputException>(() => gameService.InitialiseField("0,1"));

            Assert.Equal("Dimension values must be larger than 0", actual.Message);
        }

        [Fact]
        public void GetUserInput_InputString_ReturnString()
        {
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            input.Setup(func => func.GetUserInput())
                .Returns("1,1");
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(3, 3));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);

            var actual = gameService.GetUserInput();

            Assert.Equal("1,1", actual);
        }

        [Fact]
        public void DisplayMessage_InputString_VerifyWriteLineIsCalled()
        {
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();

            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);

            gameService.DisplayMessage(It.IsAny<string>());

            output.Verify(func => func.WriteLine(It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public void GameRound_InputQ_ReturnGameStateQuit()
        {
            var rng = new Mock<INumberGenerator>();
            rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(2, 2));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);
            gameService.InitialiseField("2,2");

            var actual = gameService.GameRound("q");

            Assert.Equal(GameState.QUIT, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnGameStatePlay()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new Mock<ICoordinateRepository>();
            coordinate.Setup(i => i.MakeCoordinate(It.IsAny<string>(), It.IsAny<Dimension>())).Returns(new Coordinate(0, 1));
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(2, 2));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);
            gameService.InitialiseField("2,2");

            var actual = gameService.GameRound("1,2");

            Assert.Equal(GameState.PLAY, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnGameStateLose()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new Mock<ICoordinateRepository>();
            coordinate.Setup(i => i.MakeCoordinate(It.IsAny<string>(), It.IsAny<Dimension>())).Returns(new Coordinate(0, 0));
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(2, 2));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);
            gameService.InitialiseField("2,2");

            var actual = gameService.GameRound("1,1");

            Assert.Equal(GameState.LOSE, actual);
        }

        [Fact]
        public void GameRound_InputCoordinate_ReturnGameStateWin()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new Mock<ICoordinateRepository>();
            coordinate.SetupSequence(i => i.MakeCoordinate(It.IsAny<string>(), It.IsAny<Dimension>()))
                        .Returns(new Coordinate(0, 1))
                        .Returns(new Coordinate(1, 0))
                        .Returns(new Coordinate(1, 1));
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(2, 2));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);
            gameService.InitialiseField("2,2");
            gameService.GameRound("1,2");
            gameService.GameRound("2,1");
            var actual = gameService.GameRound("2,2");

            Assert.Equal(GameState.WIN, actual);
        }

        [Fact]
        public void GameRound_InputInvalidCoordinate_ThrowInvalidInputException()
        {
            var rng = new Mock<INumberGenerator>();
            rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>())).Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var coordinate = new CoordinateRepository();
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(2, 2));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate);
            gameService.InitialiseField("2,2");

            var actual = Assert.Throws<InvalidInputException>(() => gameService.GameRound("0,0"));

            Assert.Equal("Coordinate must be within the field bounds", actual.Message);
        }
    }
}
