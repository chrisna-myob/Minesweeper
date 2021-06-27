using Xunit;
using Minesweeper;
using System.Collections.Generic;
using Minesweeper.Factory;
using Moq;
using Minesweeper.Service;

namespace MinesweeperTests
{
    public class FieldServiceTests
    {
        private readonly FieldService _fieldService;
        private readonly Field _field2x2;
        private readonly Field _field3x3;

        public FieldServiceTests()
        {
            var coordinateService = new CoordinateService();
            var gridFactory = new GridFactory(coordinateService);
            _fieldService = new FieldService(coordinateService);

            var mineCoordinateList = new List<Coordinate> { new Coordinate(0, 0) };

            _field2x2 = MakeField(2, gridFactory, mineCoordinateList);
            _field3x3 = MakeField(3, gridFactory, mineCoordinateList);
        }

        private Field MakeField(int dimensionValue, GridFactory gridFactory, List<Coordinate> mineCoordinates)
        {
            var dimension = new Dimension(dimensionValue, dimensionValue);
            var grid = gridFactory.MakeGrid(dimension, mineCoordinates);
            return new Field(dimension, mineCoordinates, grid);
        }

        [Fact]
        public void GetDimension_ReturnsDimension()
        {
            _fieldService.SetField(_field2x2);

            var actual = _fieldService.GetDimension();

            Assert.Equal(new Dimension(2, 2), actual);
        }

        [Fact]
        public void SetAdjacentCoordinatesToBeUncovered_InputCoordinate_VerifySquareCanBeDisplayed()
        {
            _fieldService.SetField(_field2x2);

            _fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(0, 1));
            var actual = _field2x2.SquareHasBeenUncovered(new Coordinate(0, 1));

            Assert.True(actual);
        }

        [Fact]
        public void SetAdjacentCoordinatesToBeUncovered_InputCoordinate_VerifyExpectedCoordinateHasBeenUncovered()
        {
            _fieldService.SetField(_field3x3);

            _fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(0, 2));

            Assert.True(_field3x3.SquareHasBeenUncovered(new Coordinate(0, 1)));
            Assert.True(_field3x3.SquareHasBeenUncovered(new Coordinate(1, 1)));
            Assert.True(_field3x3.SquareHasBeenUncovered(new Coordinate(2, 1)));
            Assert.True(_field3x3.SquareHasBeenUncovered(new Coordinate(0, 2)));
            Assert.True(_field3x3.SquareHasBeenUncovered(new Coordinate(1, 2)));
            Assert.True(_field3x3.SquareHasBeenUncovered(new Coordinate(2, 2)));
        }

        [Fact]
        public void HasWon_InputCoordinatesThatResultInWin_ReturnTrue()
        {
            _fieldService.SetField(_field2x2);
            _fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(0, 1));
            _fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(1, 0));
            _fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(1, 1));

            var actual = _fieldService.HasWon();

            Assert.True(actual);
        }

        [Fact]
        public void HasWon_ReturnFalse()
        {
            _fieldService.SetField(_field2x2);

            var actual = _fieldService.HasWon();

            Assert.False(actual);
        }

        [Fact]
        public void HasLost_ReturnFalse()
        {
            _fieldService.SetField(_field2x2);

            var actual = _fieldService.HasLost();

            Assert.False(actual);
        }

        [Fact]
        public void HasLost_InputCoordinateCorrespondingToAMine_ReturnTrue()
        {
            _fieldService.SetField(_field2x2);
            _field2x2.UncoverSquare(new Coordinate(0, 0));

            var actual = _fieldService.HasLost();

            Assert.True(actual);
        }

        [Fact]
        public void GridToString_InputPlayerView_ReturnCorrectString()
        {
            var expected = " ------- \n| . | . |\n ------- \n| . | . |\n ------- \n\n";
            _fieldService.SetField(_field2x2);

            var actual = _fieldService.GridToString(View.PLAYER);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridToString_InputPlayerViewWithOneSquareUncovered_ReturnCorrectString()
        {
            var expected = " ------- \n| . | 1 |\n ------- \n| . | . |\n ------- \n\n";
            _fieldService.SetField(_field2x2);
            _field2x2.UncoverSquare(new Coordinate(0, 1));

            var actual = _fieldService.GridToString(View.PLAYER);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GridToString_InputAdminView_ReturnCorrectString()
        {
            var expected = " ------- \n| * | 1 |\n ------- \n| 1 | 1 |\n ------- \n\n";
            _fieldService.SetField(_field2x2);

            var actual = _fieldService.GridToString(View.ADMIN);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void CoordinateHasAlreadyBeenUncovered_ReturnTrue()
        {
            var coordinate = new Coordinate(0, 1);
            _fieldService.SetField(_field2x2);
            _field2x2.UncoverSquare(coordinate);

            var actual = _fieldService.HasCoordinateHasAlreadyBeenUncovered(coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void CoordinateHasAlreadyBeenUncovered_ReturnFalse()
        {
            _fieldService.SetField(_field2x2);

            var actual = _fieldService.HasCoordinateHasAlreadyBeenUncovered(new Coordinate(0, 1));

            Assert.False(actual);
        }
    }
}
