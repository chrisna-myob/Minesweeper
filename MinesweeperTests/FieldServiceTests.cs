using System.Collections.Generic;
using Xunit;
using Moq;
using Minesweeper;
using Minesweeper.Repository;

namespace MinesweeperTests
{
    public class FieldServiceTests
    {
        [Fact]
        public void SetAdjacentCoordinatesInFieldToShow_InputOneCoordinateWhichHasHintLargerThanZero_ValidateCorrectSquaresHaveBeenShown()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(rng => rng.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(0)
                .Returns(0);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(new Dimension(2, 2));
            var fieldRepo = new FieldRepository(field);
            var fieldService = new FieldService(fieldRepo);

            fieldService.SetAdjacentCoordinatesInFieldToShow(new Coordinate(0, 1));

            var actual = field.CanShowSquare(new Coordinate(0, 1));

            Assert.True(actual);
        }

        [Fact]
        public void SetAdjacentCoordinatesInFieldToShow_ValidateCorrectSquaresHaveBeenShown()
        {
            var rng = new Mock<INumberGenerator>();
            rng.SetupSequence(rng => rng.GetRandomNumber(It.IsAny<int>(), It.IsAny<int>()))
                .Returns(1)
                .Returns(1)
                .Returns(2);
            var builder = new FieldBuilder(rng.Object);
            var field = builder.CreateField(new Dimension(3, 3));
            var fieldRepo = new FieldRepository(field);
            var fieldService = new FieldService(fieldRepo);

            fieldService.SetAdjacentCoordinatesInFieldToShow(new Coordinate(0, 0));

            Assert.True(field.CanShowSquare(new Coordinate(0, 0)));
            Assert.True(field.CanShowSquare(new Coordinate(0, 1)));
            Assert.True(field.CanShowSquare(new Coordinate(1, 0)));
            Assert.True(field.CanShowSquare(new Coordinate(1, 1)));
            Assert.True(field.CanShowSquare(new Coordinate(2, 0)));
            Assert.True(field.CanShowSquare(new Coordinate(2, 1)));
        }

        [Fact]
        public void ToString_ReturnStringOfField()
        {
            var expected = "..\n..\n";
            var fieldRepo = new Mock<IFieldRepository>();
            fieldRepo.Setup(i => i.GetDimension())
                    .Returns(new Dimension(2, 2));
            fieldRepo.SetupSequence(i => i.CanShowSquare(It.IsAny<Coordinate>()))
                    .Returns(false)
                    .Returns(false)
                    .Returns(false)
                    .Returns(false);
            var fieldService = new FieldService(fieldRepo.Object);

            var actual = fieldService.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void UncoveredBoardToString_ReturnStringOfField()
        {
            var expected = "*1\n11\n";
            var fieldRepo = new Mock<IFieldRepository>();
            fieldRepo.Setup(i => i.GetDimension())
                    .Returns(new Dimension(2, 2));
            fieldRepo.SetupSequence(i => i.CanShowSquare(It.IsAny<Coordinate>()))
                    .Returns(true)
                    .Returns(true)
                    .Returns(true)
                    .Returns(true);
            fieldRepo.SetupSequence(i => i.GetSquareValue(It.IsAny<Coordinate>()))
                    .Returns("*")
                    .Returns("1")
                    .Returns("1")
                    .Returns("1");
            var fieldService = new FieldService(fieldRepo.Object);

            var actual = fieldService.ToString();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_ReturnTrue()
        {
            var fieldRepo = new Mock<IFieldRepository>();
            fieldRepo.Setup(i => i.GetDimension())
                    .Returns(new Dimension(2, 2));
            fieldRepo.SetupSequence(i => i.CanShowSquare(It.IsAny<Coordinate>()))
                    .Returns(false)
                    .Returns(true)
                    .Returns(true)
                    .Returns(true);
            fieldRepo.Setup(i => i.CoordinateHasMine(It.IsAny<Coordinate>()))
                    .Returns(true);
            fieldRepo.SetupSequence(i => i.NumberOfMines())
                    .Returns(1);

            var fieldService = new FieldService(fieldRepo.Object);

            var actual = fieldService.RemainingSquaresAreMines();

            Assert.True(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_ReturnFalse()
        {
            var fieldRepo = new Mock<IFieldRepository>();
            fieldRepo.Setup(i => i.GetDimension())
                    .Returns(new Dimension(2, 2));
            fieldRepo.SetupSequence(i => i.CanShowSquare(It.IsAny<Coordinate>()))
                    .Returns(true)
                    .Returns(true)
                    .Returns(true)
                    .Returns(true);
            fieldRepo.SetupSequence(i => i.NumberOfMines())
                    .Returns(1);

            var fieldService = new FieldService(fieldRepo.Object);

            var actual = fieldService.RemainingSquaresAreMines();

            Assert.False(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_ReturnTrue()
        {
            var fieldRepo = new Mock<IFieldRepository>();
            fieldRepo.SetupSequence(i => i.CanShowSquare(It.IsAny<Coordinate>()))
                    .Returns(true);
            fieldRepo.Setup(i => i.GetMineCoordinates())
                    .Returns(new List<Coordinate> { new Coordinate(0, 0) });

            var fieldService = new FieldService(fieldRepo.Object);

            var actual = fieldService.MineHasBeenUncovered();

            Assert.True(actual);
        }

        [Fact]
        public void MineHasBeenUncovered_ReturnFalse()
        {
            var fieldRepo = new Mock<IFieldRepository>();
            fieldRepo.SetupSequence(i => i.CanShowSquare(It.IsAny<Coordinate>()))
                    .Returns(false);
            fieldRepo.Setup(i => i.GetMineCoordinates())
                    .Returns(new List<Coordinate> { new Coordinate(0, 0) });

            var fieldService = new FieldService(fieldRepo.Object);

            var actual = fieldService.MineHasBeenUncovered();

            Assert.False(actual);
        }
    }
}
