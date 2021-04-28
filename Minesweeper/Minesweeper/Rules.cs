using System;
using Minesweeper;
namespace Minesweeper
{
    public static class Rules
    {
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
            for(var row = 0; row < field.NumberOfRows; row++)
            {
                for(var col = 0; col < field.NumberOfColumns; col++)
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
                if (field[coord.X, coord.Y].RevealSquare() != "0")
                {
                    field[coord.X, coord.Y].SetSquareToShow();
                } else
                {
                    Sprawl(fieldObject, coord.X, coord.Y);
                }
            }
        }

        public static void Sprawl(Field fieldObject, int row, int col)
        {
            var field = fieldObject.GetField();
            var numRows = fieldObject.NumberOfRows;
            var numCols = fieldObject.NumberOfColumns;
            if (field[row, col].CanShow == true)
            {
                return;
            }
            if (field[row, col].CanShow == false && field[row, col].RevealSquare() != "0")
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
