using Xunit;
using Minesweeper;
using System.Collections.Generic;
using Minesweeper.Factory;
using Moq;

namespace MinesweeperTests
{
    public class FieldServiceTests
    {
        private readonly GridFactory _gridFactory;

        public FieldServiceTests()
        {
            _gridFactory = new GridFactory();
        }

        [Fact]
        public void GetDimension_ReturnsDimension()
        {
            var dimension = new Dimension(2, 2);
            var coordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, coordinate);
            var field = new Field(dimension, coordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            var actual = fieldService.GetDimension();

            Assert.Equal(dimension, actual);
        }

        [Fact]
        public void SetAdjacentCoordinatesToBeUncovered_InputCoordinate_VerifySquareCanBeDisplayed()
        {
            var dimension = new Dimension(2, 2);
            var coordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, coordinate);
            var field = new Field(dimension, coordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(0, 1));
            var actual = field.SquareHasBeenUncovered(new Coordinate(0, 1));

            Assert.True(actual);
        }

        [Fact]
        public void SetAdjacentCoordinatesToBeUncovere_InputCoordinate_VerifyMultipleSquaresCanBeDisplayed()
        {
            var dimension = new Dimension(3, 3);
            var coordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, coordinate);
            var field = new Field(dimension, coordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            fieldService.SetAdjacentCoordinatesToBeUncovered(new Coordinate(0, 2));

            Assert.True(field.SquareHasBeenUncovered(new Coordinate(0, 1)));
            Assert.True(field.SquareHasBeenUncovered(new Coordinate(1, 1)));
            Assert.True(field.SquareHasBeenUncovered(new Coordinate(2, 1)));
            Assert.True(field.SquareHasBeenUncovered(new Coordinate(0, 2)));
            Assert.True(field.SquareHasBeenUncovered(new Coordinate(1, 2)));
            Assert.True(field.SquareHasBeenUncovered(new Coordinate(2, 2)));
        }

        [Fact]
        public void HasWon_InputCoordinatesThatResultInWin_ReturnTrue()
        {
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);
            var coordinate = new Coordinate(0, 1);
            var coordinate2 = new Coordinate(1, 0);
            var coordinate3 = new Coordinate(1, 1);
            fieldService.SetAdjacentCoordinatesToBeUncovered(coordinate);
            fieldService.SetAdjacentCoordinatesToBeUncovered(coordinate2);
            fieldService.SetAdjacentCoordinatesToBeUncovered(coordinate3);

            var actual = fieldService.HasWon();

            Assert.True(actual);
        }

        [Fact]
        public void HasWon_ReturnFalse()
        {
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            var actual = fieldService.HasWon();

            Assert.False(actual);
        }

        [Fact]
        public void HasLost_ReturnFalse()
        {
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            var actual = fieldService.HasLost();

            Assert.False(actual);
        }

        [Fact]
        public void HasLost_InputCoordinateCorrespondingToAMine_ReturnTrue()
        {
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);
            var coordinate = new Coordinate(0, 0);
            field.UncoverSquare(coordinate);

            var actual = fieldService.HasLost();

            Assert.True(actual);
        }

        [Fact]
        public void CoordinateHasAlreadyBeenUsed_InputCoordinateThatCanAlreadyBeShown_ThrowInvalidInputException()
        {
            var coordinate = new Coordinate(0, 1);
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);
            field.UncoverSquare(coordinate);

            var exception = Assert.Throws<InvalidInputException>(() => fieldService.CoordinateHasAlreadyBeenUncovered(coordinate));
            Assert.Equal("You have already entered this coordinate.\n", exception.Message);
        }

        [Fact]
        public void BoardToString_InputPlayerView_ReturnCorrectString()
        {
            var expected = " ------- \n| . | . |\n ------- \n| . | . |\n ------- \n\n";
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            var actual = fieldService.BoardToString(View.PLAYER);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BoardToString_InputPlayerViewWithOneSquareUncovered_ReturnCorrectString()
        {
            var expected = " ------- \n| . | 1 |\n ------- \n| . | . |\n ------- \n\n";
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);
            var coordinate = new Coordinate(0, 1);
            field.UncoverSquare(coordinate);

            var actual = fieldService.BoardToString(View.PLAYER);

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void BoardToString_InputAdminView_ReturnCorrectString()
        {
            var expected = " ------- \n| * | 1 |\n ------- \n| 1 | 1 |\n ------- \n\n";
            var dimension = new Dimension(2, 2);
            var mineCoordinate = new List<Coordinate> { new Coordinate(0, 0) };
            var grid = _gridFactory.MakeGrid(dimension, mineCoordinate);
            var field = new Field(dimension, mineCoordinate, grid);
            var fieldService = new FieldService();
            fieldService.SetField(field);

            var actual = fieldService.BoardToString(View.ADMIN);

            Assert.Equal(expected, actual);
        }
    }
}
