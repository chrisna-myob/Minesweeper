﻿using System;
using Minesweeper;
namespace Minesweeper
{
    public static class Rules
    {
        private const string NO_HINT = "0";

        public static bool CoordinateHasMineSquare(Field field, Coordinate coord)
        {
            var square = field.GetSquareFromCoordinate(coord);
            if (square.HasMine()) return true;
            return false;
        }

        public static bool RemainingSquaresAreMines(Field field)
        {
            var currentField = field.GetField();
            var countOfMines = 0;
            for(var row = 0; row < field.Dimension.NumRows; row++)
            {
                for(var col = 0; col < field.Dimension.NumCols; col++)
                {
                    if (currentField[row, col].CanShow == false && !currentField[row, col].HasMine())
                    {
                        return false;
                    }
                    if (currentField[row,col].CanShow == false && currentField[row, col].HasMine()) countOfMines++;
                }
            }

            if (countOfMines == field.NumberOfMines) return true;
            else return false;
        }

        public static void SetCoordinatesToShow(Field fieldObject, Coordinate coord)
        {
            var field = fieldObject.GetField();
            if (field[coord.X, coord.Y].CanShow == false)
            {
                if (field[coord.X, coord.Y].RevealSquare() != NO_HINT)
                {
                    field[coord.X, coord.Y].SetSquareToShow();
                } else
                {
                    Sprawl(fieldObject, coord.X, coord.Y);
                }
            }
        }

        public static bool MineHasBeenUncovered(Field field)
        {
            var currentField = field.GetField();
            for (var row = 0; row < field.Dimension.NumRows; row++)
            {
                for (var col = 0; col < field.Dimension.NumCols; col++)
                {
                    if (currentField[row, col].CanShow && currentField[row, col].HasMine())
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static void Sprawl(Field fieldObject, int row, int col)
        {
            var field = fieldObject.GetField();
            var numRows = fieldObject.Dimension.NumRows;
            var numCols = fieldObject.Dimension.NumCols;
            if (field[row, col].CanShow == true)
            {
                return;
            }
            if (field[row, col].CanShow == false && field[row, col].RevealSquare() != NO_HINT)
            {
                field[row, col].SetSquareToShow();
                return;
            }
            else
            {
                field[row, col].SetSquareToShow();
                if (row - 1 >= 0 && col - 1 >= 0)
                {
                    Sprawl(fieldObject, row - 1, col - 1);
                }
                if (row - 1 >= 0 && col + 1 < numCols)
                {
                    Sprawl(fieldObject, row - 1, col + 1);
                }
                if (row + 1 < numRows && col - 1 >= 0)
                {
                    Sprawl(fieldObject, row + 1, col - 1);
                }
                if (row + 1 < numRows && col + 1 < numCols)
                {
                    Sprawl(fieldObject, row + 1, col + 1);
                }
                if (row - 1 >= 0)
                {
                    Sprawl(fieldObject, row - 1, col);
                }
                if (col - 1 >= 0)
                {
                    Sprawl(fieldObject, row, col - 1);
                }
                if (row + 1 < numRows)
                {
                    Sprawl(fieldObject, row + 1, col);
                }
                if (col + 1 < numCols)
                {
                    Sprawl(fieldObject, row, col + 1);
                }
            }
        }
    }
}