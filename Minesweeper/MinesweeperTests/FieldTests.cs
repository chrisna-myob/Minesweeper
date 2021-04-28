using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class FieldTests
    {
        [Fact]
        public void Field_InputRowsColumnsListOfMineCoordinatesAndNumberOfMines_ReturnFieldObject()
        {
            var numRows = 1;
            var numCols = 2;
            var numMines = 1;
            var field = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(0, 0) }, numMines);

            var actual = new Field(numRows, numCols, numMines, field);

            Assert.Equal(numRows, actual.NumberOfRows);
            Assert.Equal(numCols, actual.NumberOfColumns);
            Assert.Equal(numMines, actual.NumberOfMines);
            Assert.Equal(field, actual.GetField());
        }

        [Fact]
        public void GetSquareFromCoordinate_InputCoordinate_ReturnSquare()
        {
            var coordinate = new Coordinate(0,0);
            var numRows = 1;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, null, numMines);
            var field = new Field(numRows, numCols, numMines, fieldBuild);

            var actual = field.GetSquareFromCoordinate(coordinate);

            Assert.False(actual.HasMine());
        }

        [Fact]
        public void SetSquareToShowUsingCoordinate_InputCoordinate_ValidateSquareCanBeShown()
        {
            var coordinate = new Coordinate(0, 0);
            var numRows = 1;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, null, numMines);
            var field = new Field(numRows, numCols, numMines, fieldBuild);

            field.SetSquareToShowWithCoordinate(coordinate);

            var actual = field.GetSquareFromCoordinate(coordinate);

            Assert.True(actual.CanShow);
        }
    }
}
