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
        //[Fact]
        //public void MakeDimension_InputDimensionAsString_ReturnDimensionObject()
        //{
        //    var io = new Mock<IInputRepository>();
        //    var dimensionRepo = new DimensionRepository();
        //    io.Setup(io => io.GetUserInput())
        //        .Returns("2,2");

        //    var gameService = new GameService(io.Object, null, new Mock<IOutputRepository>().Object, dimensionRepo, null);

        //    var actual = gameService.MakeDimension();

        //    Assert.Equal(new Dimension(2, 2), actual);
        //}

        //[Fact]
        //public void CreateFieldService_InputDimension_ReturnFieldServiceObject()
        //{
        //    var dimensionRepo = new DimensionRepository();
        //    var dimension = new Dimension(2, 2);
        //    var rng = new Mock<INumberGenerator>();
        //    rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
        //       .Returns(new Queue<int>(new[] { 0, 0 }).Dequeue);
        //    var builder = new FieldBuilder(rng.Object);

        //    var io = new Mock<IInputRepository>();

        //    var gameService = new GameService(io.Object, builder, new Mock<IOutputRepository>().Object, dimensionRepo, null);

        //    var actual = gameService.CreateFieldService(new Dimension(2, 2));

        //    Assert.Equal(typeof(FieldService), actual.GetType());
        //}

        [Fact]
        public void MakeCoordinate_InputString_Return()
        {
            //var dimension = new Dimension(2, 2);
            //var dimensionRepo = new DimensionRepository();
            //var coordRepo = new CoordinateRepository();
            //var rng = new Mock<INumberGenerator>();
            //rng.Setup(i => i.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
            //   .Returns(new Queue<int>(new[] { 0, 0 }).Dequeue);
            //var builder = new FieldBuilder(rng.Object);

            //var io = new Mock<IInputRepository>();

            //var gameService = new GameService(io.Object, builder, new Mock<IOutputRepository>().Object, dimensionRepo, coordRepo);

            //gameService.CreateFieldService(dimension);

            //var actual = gameService.MakeCoordinate("1,1");

            //Assert.Equal(new Coordinate(0, 0), actual);
        }

        [Fact]
        public void InitialiseField()
        {
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var output = new Mock<IOutputRepository>();
            var input = new Mock<IInputRepository>();
            var dimension = new DimensionRepository();
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();
            dimensionRepo.Setup(funct => funct.MakeDimension(It.IsAny<string>()))
                .Returns(new Dimension(3,3));
            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);

            gameService.InitialiseField(It.IsAny<string>());

            //var actual = Assert.Throws<InvalidInputException>(() => Validate.IsCoordinateInputValid(new Dimension(2, 2), "-1,1"));

            dimensionRepo.Verify(func => func.MakeDimension(It.IsAny<string>()), Times.Once());
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
            var dimension = new DimensionRepository();
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
            var dimension = new DimensionRepository();
            var coordinate = new Mock<ICoordinateRepository>();
            var dimensionRepo = new Mock<IDimensionRepository>();

            var gameService = new GameService(input.Object, builder, output.Object, dimensionRepo.Object, coordinate.Object);

            gameService.DisplayMessage(It.IsAny<string>());

            output.Verify(func => func.WriteLine(It.IsAny<string>()), Times.Once());
        }
    }
}
