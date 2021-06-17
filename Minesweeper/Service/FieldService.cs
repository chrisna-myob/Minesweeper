using System;
using System.Collections.Generic;
using Minesweeper.Factory;
using Minesweeper.Service;

namespace Minesweeper
{
    public class FieldService
    {
        private Field _field;
        private CoordinateService _coordinateService;

        public FieldService(CoordinateService coordinateService)
        {
            _coordinateService = coordinateService;
        }

        public void SetField(Field field)
        {
            _field = field;
        }

        public Dimension GetDimension()
        {
            return _field.Dimension;
        }

        public bool HasWon()
        {
            var countOfMines = 0;
            for (var row = 0; row < GetDimension().NumRows; row++)
            {
                for (var col = 0; col < GetDimension().NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    if (_field.SquareHasBeenUncovered(coordinate) == false)
                    {
                        if (_field.SquareHasMine(coordinate)) countOfMines++;
                        else return false;
                    }

                }
            }
            if (countOfMines == _field.NumberOfMines) return true;
            return false;
        }

        public bool HasLost()
        {
            foreach (var mineCoordinate in _field.MineCoordinates)
            {
                if (_field.SquareHasBeenUncovered(mineCoordinate)) return true;
            }
            return false;
        }

        public string BoardToString(View view = View.PLAYER)
        {
            var lineBreak = GetGridLine(GetDimension().NumCols);
            var stringBuilder = lineBreak;
            for (var row = 0; row < GetDimension().NumRows; row++)
            {
                stringBuilder += "|";
                for (var col = 0; col < GetDimension().NumCols; col++)
                {
                    var coord = new Coordinate(row, col);
                    stringBuilder += _field.GetSquareAsString(coord, view);
                }
                stringBuilder += Environment.NewLine;
                stringBuilder += lineBreak;
            }
            return stringBuilder += Environment.NewLine;
        }



        private string GetGridLine(int num)
        {
            var numberOfDashes = num * 3 + num - 1;
            var stringBuilder = " ";
            for (var i = 0; i < numberOfDashes; i++)
            {
                stringBuilder += "-";
            }
            return stringBuilder += " \n";
        }

        public void CoordinateHasAlreadyBeenUncovered(Coordinate coord)
        {
            if (_field.SquareHasBeenUncovered(coord))
            {
                throw new InvalidInputException("You have already entered this coordinate.\n");
            }
        }

        public void SetAdjacentCoordinatesToBeUncovered(Coordinate coordinate)
        {
            if (_field.SquareHasBeenUncovered(coordinate)) return;
            else
            {
                _field.UncoverSquare(coordinate);
                if (_field.SquareHasNoHint(coordinate) == false) return;
                else
                {
                    var adjacentSquaresList = _coordinateService.GetAdjacentCoordinates(coordinate, _field.Dimension);
                    foreach (var coord in adjacentSquaresList)
                    {
                        SetAdjacentCoordinatesToBeUncovered(coord);
                    }
                }
            }
        }
    }
}
