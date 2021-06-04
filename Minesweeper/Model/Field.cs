using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private int _numberOfMines;
        private ISquare[,] _board;
        private List<Coordinate> _mineCoordinates;
        private const string NO_HINT = "0";

        public Dimension Dimension => _dimension;
        public int NumberOfMines => _numberOfMines;

        public Field(Dimension dimension, int mines, ISquare[,] board, List<Coordinate> mineCoordinates)
        {
            _dimension = dimension;
            _numberOfMines = mines;
            _board = board;
            _mineCoordinates = mineCoordinates;
        }

        public bool CanShowSquare(Coordinate coord)
        {
            return _board[coord.X, coord.Y].CanShow;
        }

        private ISquare GetSquareFromCoordinate(Coordinate coord)
        {
            return _board[coord.X, coord.Y];
        }
        
        public override bool Equals(object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Field f = (Field)obj;
                if (NumberOfMines == f.NumberOfMines)
                {
                    for (var index = 0; index < _mineCoordinates.Count; index++)
                    {
                        if (_mineCoordinates[index].X != f._mineCoordinates[index].X && _mineCoordinates[index].Y != f._mineCoordinates[index].Y) return false;
                    }
                    return (Dimension.NumRows == f.Dimension.NumRows) && (Dimension.NumCols == f.Dimension.NumCols);
                }
                return false;
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }

        public void SetAdjacentCoordinatesInFieldToShow(Coordinate coordinate)
        {
            var square = GetSquareFromCoordinate(coordinate);
            if (square.CanShow) return;
            else
            {
                square.SetSquareToShow();
                if (square.GetSquareValue() != NO_HINT) return;
                else
                {
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate.X, coordinate.Y, Dimension);
                    foreach (var coord in adjacentSquaresList)
                    {
                        SetAdjacentCoordinatesInFieldToShow(coord);
                    }
                }
            }
        }

        public bool RemainingSquaresAreMines()
        {
            var countOfMines = 0;
            for (var row = 0; row < Dimension.NumRows; row++)
            {
                for (var col = 0; col < Dimension.NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    var square = GetSquareFromCoordinate(coordinate);
                    if (square.CanShow == false)
                    {
                        if (square.HasMine()) countOfMines++;
                        else if (!square.HasMine()) return false;
                    }

                }
            }
            if (countOfMines == _numberOfMines) return true;
            return false;
        }

        public bool MineHasBeenUncovered()
        {
            foreach (var coord in _mineCoordinates)
            {
                var square = GetSquareFromCoordinate(coord);
                if (square.CanShow) return true;
            }
            return false;
        }

        public override string ToString()
        {
            var lineBreak = GlobalHelpers.Lines(Dimension.NumCols);
            var stringBuilder = lineBreak;
            for (var row = 0; row < Dimension.NumRows; row++)
            {
                stringBuilder += "|";
                for (var col = 0; col < Dimension.NumCols; col++)
                {
                    var coord = new Coordinate(row, col);
                    var square = GetSquareFromCoordinate(coord);

                    if (square.CanShow)
                    {
                        if (square.GetSquareValue() == "0") stringBuilder += "   |";
                        else stringBuilder += $" {square.GetSquareValue()} |";
                    }
                    else
                    {
                        stringBuilder += " . |";
                    }

                }
                stringBuilder += Environment.NewLine;
                stringBuilder += lineBreak;
            }
            return stringBuilder + "\n";
        }

        public string UncoveredBoardToString()
        {
            var lineBreak = GlobalHelpers.Lines(Dimension.NumCols);
            var stringBuilder = lineBreak;
            for (var row = 0; row < Dimension.NumRows; row++)
            {
                stringBuilder += "|";
                for (var col = 0; col < Dimension.NumCols; col++)
                {
                    var coord = new Coordinate(row, col);
                    var square = GetSquareFromCoordinate(coord);
                    if (square != null)
                    {
                        stringBuilder += $" {square.GetSquareValue()} |";
                    } else
                    {
                        stringBuilder += "   |";
                    }
                }
                stringBuilder += Environment.NewLine;
                stringBuilder += lineBreak;
            }

            return stringBuilder + "\n";
        }
    }
}