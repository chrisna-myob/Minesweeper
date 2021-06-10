using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private ISquare[,] _grid;
        private List<Coordinate> _mineCoordinates;

        public Dimension Dimension => _dimension;
        public int NumberOfMines => _mineCoordinates.Count;
        public List<Coordinate> MineCoordinates => _mineCoordinates;

        public Field(Dimension dimension, List<Coordinate> mineCoordinates, ISquare[,] grid)
        {
            _dimension = dimension;
            _mineCoordinates = mineCoordinates;
            _grid = grid;
        }

        private ISquare GetSquareFromCoordinate(Coordinate coord)
        {
            return _grid[coord.X, coord.Y];
        }

        public bool SquareHasBeenUncovered(Coordinate coord)
        {
            return GetSquareFromCoordinate(coord).HasBeenUncovered;
        }

        public void UncoverSquare(Coordinate coord)
        {
            GetSquareFromCoordinate(coord).Uncover();
        }

        public bool SquareHasMine(Coordinate coord)
        {
            return GetSquareFromCoordinate(coord).HasMine();
        }

        public string GetSquareValue(Coordinate coord)
        {
            return GetSquareFromCoordinate(coord).GetSquareValue();
        }

        public string GetSquareAsString(Coordinate coord, View view)
        {
            return GetSquareFromCoordinate(coord).GetSquareAsString(view);
        }

        public bool SquareHasNoHint(Coordinate coord)
        {
            return GetSquareFromCoordinate(coord).GetSquareValue() == " ";
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
    }
}