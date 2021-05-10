using System.Collections.Generic;

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
                    if (!CanShowSquare(currentField, row, col) && !SquareHasMine(currentField, row, col))
                    {
                        return false;
                    }
                    if (!CanShowSquare(currentField, row, col) && SquareHasMine(currentField, row, col)) countOfMines++;
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

        public static bool CanDisplayIndividualSquare(Field fieldObject, Coordinate coord)
        {
            var field = fieldObject.GetField();
            if (field[coord.X, coord.Y].CanShow == false && field[coord.X, coord.Y].RevealSquare() != NO_HINT)
            {
                return true;
            }
            return false;
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

            if (CanShowSquare(field, row, col)) return;
            else if (!CanShowSquare(field, row, col) && SquareHasHintLargerThanZero(field, row, col))
            {
                field[row, col].SetSquareToShow();
                return;
            } else
            {
                field[row, col].SetSquareToShow();

                var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(row, col, fieldObject.Dimension);

                foreach (var coord in adjacentSquaresList)
                {
                    Sprawl(fieldObject, coord.X, coord.Y);
                }
            }
        }

        private static bool CanShowSquare(ISquare[,] field, int row, int col)
        {
            return field[row, col].CanShow;
        }

        private static bool SquareHasHintLargerThanZero(ISquare[,] field, int row, int col)
        {
            return field[row, col].RevealSquare() != NO_HINT;
        }

        private static bool SquareHasMine(ISquare[,] field, int row, int col)
        {
            return field[row, col].HasMine();
        }
    }
}
