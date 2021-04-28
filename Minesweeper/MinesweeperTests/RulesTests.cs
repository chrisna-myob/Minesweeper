using System;
using System.Collections.Generic;
using Minesweeper;
using Xunit;
using Moq;

namespace MinesweeperTests
{
    public class RulesTests
    {
        [Fact]
        public void CoordinateHasMineSquare_InputFieldAndCoordinate_ReturnExpectedBool()
        {
            var coordinate = new Coordinate(0,0);
            var numRows = 1;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(0, 0) }, numMines);

            var field = new Field(numRows, numCols, numMines, fieldBuild);

            var actual = Rules.CoordinateHasMineSquare(field, coordinate);

            Assert.True(actual);
        }

        [Fact]
        public void CoordinateHasMineSquare_InputFieldAndCoordinate_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var numRows = 1;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(0, 1) }, numMines);

            var field = new Field(numRows, numCols, numMines, fieldBuild);

            var actual = Rules.CoordinateHasMineSquare(field, coordinate);

            Assert.False(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnTrue()
        {
            var coordinate = new Coordinate(0, 0);
            var numRows = 2;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(0, 0) }, numMines);
            var field = new Field(numRows, numCols, numMines, fieldBuild);
            field.SetSquareToShowWithCoordinate(new Coordinate(0,1));
            field.SetSquareToShowWithCoordinate(new Coordinate(1,0));
            field.SetSquareToShowWithCoordinate(new Coordinate(1,1));

            var actual = Rules.RemainingSquaresAreMines(field);

            Assert.True(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnFalse()
        {
            var coordinate = new Coordinate(0, 0);
            var numRows = 2;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(1, 0) }, numMines);
            var field = new Field(numRows, numCols, numMines, fieldBuild);
            field.SetSquareToShowWithCoordinate(new Coordinate(0, 1));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 0));
            field.SetSquareToShowWithCoordinate(new Coordinate(1, 1));

            var actual = Rules.RemainingSquaresAreMines(field);

            Assert.False(actual);
        }

        [Fact]
        public void RemainingSquaresAreMines_InputField_ReturnFalseAgain()
        {
            var coordinate = new Coordinate(0, 0);
            var numRows = 2;
            var numCols = 2;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(0, 0), new Coordinate(1, 0) }, numMines);
            var field = new Field(numRows, numCols, numMines, fieldBuild);
            field.SetSquareToShowWithCoordinate(new Coordinate(0, 1));

            var actual = Rules.RemainingSquaresAreMines(field);

            Assert.False(actual);
        }

        [Fact]
        public void SetCoordinateToShow_InputFieldAndCoordinateWithHintLargerThanZero()
        {

            var numberOfMines = 1;

            var fieldBuild = FieldBuilder.MakeField(3, 3, new List<Coordinate> { new Coordinate(0, 0) }, numberOfMines);
            FieldBuilder.CalculateHints(fieldBuild, 3, 3);
            var field = new Field(3, 3, 1, fieldBuild);
            Rules.SetCoordinatesToShow(field, new Coordinate(0,1));

            var actual = field.GetSquareFromCoordinate(new Coordinate(0,1));
            Assert.True(actual.CanShow);
        }

        [Fact]
        public void Sprawl_InputFieldAndCoordinateWithHintEqualToZero()
        {

            var coordinate = new Coordinate(0, 0);
            var numRows = 3;
            var numCols = 3;
            var numMines = 1;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(1, 2) }, numMines);
            FieldBuilder.CalculateHints(fieldBuild, 3, 3);
            var field = new Field(numRows, numCols, numMines, fieldBuild);
            Rules.SetCoordinatesToShow(field, coordinate);

            var actual = field.GetField();

            Assert.True(actual[0, 0].CanShow);
            Assert.True(actual[0, 1].CanShow);
            Assert.False(actual[0, 2].CanShow);
            Assert.True(actual[1, 0].CanShow);
            Assert.True(actual[1, 1].CanShow);
            Assert.True(actual[2, 0].CanShow);
            Assert.True(actual[2, 1].CanShow);
            Assert.False(actual[2, 2].CanShow);
            Assert.False(actual[1, 2].CanShow);
        }

        [Fact]
        public void Sprawl_InputFieldAndCoordinateWithHintEqualToZero_LargerMap()
        {

            var coordinate = new Coordinate(0, 0);
            var numRows = 4;
            var numCols = 4;
            var numMines = 2;
            var fieldBuild = FieldBuilder.MakeField(numRows, numCols, new List<Coordinate> { new Coordinate(0, 3), new Coordinate(2, 1) }, numMines);
            FieldBuilder.CalculateHints(fieldBuild, 4, 4);
            var field = new Field(numRows, numCols, numMines, fieldBuild);
            Rules.SetCoordinatesToShow(field, coordinate);

            var actual = field.GetField();

            Assert.True(actual[0, 0].CanShow);
            Assert.True(actual[0, 1].CanShow);
            Assert.True(actual[0, 2].CanShow);
            Assert.False(actual[0, 3].CanShow);
            Assert.True(actual[1, 0].CanShow);
            Assert.True(actual[1, 1].CanShow);
            Assert.True(actual[1, 2].CanShow);
            Assert.False(actual[1, 3].CanShow);
            Assert.False(actual[2, 0].CanShow);
            Assert.False(actual[2, 1].CanShow);
            Assert.False(actual[2, 2].CanShow);
            Assert.False(actual[2, 3].CanShow);
            Assert.False(actual[3, 0].CanShow);
            Assert.False(actual[3, 1].CanShow);
            Assert.False(actual[3, 2].CanShow);
            Assert.False(actual[3, 3].CanShow);
        }
    }
}
