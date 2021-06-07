using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class FieldService
    {
        private Field _field;

        public FieldService(Field field)
        {
            _field = field;
        }

        public Dimension GetDimension()
        {
            return _field.Dimension;
        }

        public void HandleCoordinate(Coordinate coord)
        {
            SetAdjacentCoordinatesToBeUncovered(coord);
        }

        private void SetAdjacentCoordinatesToBeUncovered(Coordinate coordinate)
        {
            if (_field.SquareCanBeDisplayed(coordinate)) return;
            else
            {
                _field.UncoverSquare(coordinate);
                if (_field.SquareHasHintOfZero(coordinate) == false) return;
                else
                {
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate, _field.Dimension);
                    foreach (var coord in adjacentSquaresList)
                    {
                        SetAdjacentCoordinatesToBeUncovered(coord);
                    }
                }
            }
        }

        public bool HasWon()
        {
            var countOfMines = 0;
            for (var row = 0; row < GetDimension().NumRows; row++)
            {
                for (var col = 0; col < GetDimension().NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    if (_field.SquareCanBeDisplayed(coordinate) == false)
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
            foreach (var coord in _field.MineCoordinates)
            {
                if (_field.SquareCanBeDisplayed(coord)) return true;
            }
            return false;
        }

        public void CoordinateHasAlreadyBeenUncovered(Coordinate coord)
        {
            if (_field.SquareCanBeDisplayed(coord))
            {
                throw new InvalidInputException("You have already entered this coordinate.\n");
            }
        }

        public string BoardToString(View view = View.PLAYER)
        {
            var lineBreak = GlobalHelpers.Lines(GetDimension().NumCols);
            var stringBuilder = lineBreak;
            for (var row = 0; row < GetDimension().NumRows; row++)
            {
                stringBuilder += "|";
                for (var col = 0; col < GetDimension().NumCols; col++)
                {
                    var coord = new Coordinate(row, col);
                    var square = _field.GetSquareFromCoordinate(coord);
                    if (view == View.PLAYER)
                    {
                        if (square.CanBeDisplayed)
                        {
                            if (_field.SquareHasHintOfZero(coord)) stringBuilder += "   |";
                            else stringBuilder += $" {_field.GetSquareValue(coord)} |";
                        }
                        else
                        {
                            stringBuilder += " . |";
                        }
                    } else
                    {
                        if (square != null)
                        {
                            stringBuilder += $" {_field.GetSquareValue(coord)} |";
                        }
                        else
                        {
                            stringBuilder += "   |";
                        }
                    }
                }
                stringBuilder += Environment.NewLine;
                stringBuilder += lineBreak;
            }
            return stringBuilder + "\n";

        }
    }
}
