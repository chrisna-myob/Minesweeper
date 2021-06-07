using System;
using System.Collections.Generic;

namespace Minesweeper
{
    public class Field
    {
        private Dimension _dimension;
        private ISquare[,] _board;
        private List<Coordinate> _mineCoordinates;

        public Dimension Dimension => _dimension;
        public int NumberOfMines => _mineCoordinates.Count;
        public List<Coordinate> MineCoordinates => _mineCoordinates;

        public Field(Dimension dimension, List<Coordinate> mineCoordinates)
        {
            _dimension = dimension;
            _mineCoordinates = mineCoordinates;
            _board = new ISquare[dimension.NumRows, dimension.NumCols];
            InitialiseBoard();
            CalculateHints();
        }

        public bool SquareCanBeDisplayed(Coordinate coord)
        {
            return _board[coord.X, coord.Y].CanBeDisplayed;
        }

        public ISquare GetSquareFromCoordinate(Coordinate coord)
        {
            return _board[coord.X, coord.Y];
        }

        public void UncoverSquare(Coordinate coord)
        {
            _board[coord.X, coord.Y].Uncover();
        }

        public bool SquareHasMine(Coordinate coord)
        {
            return _board[coord.X, coord.Y].HasMine();
        }

        public string GetSquareValue(Coordinate coord)
        {
            return _board[coord.X, coord.Y].GetSquareValue();
        }

        public bool SquareHasHintOfZero(Coordinate coord)
        {
            return _board[coord.X, coord.Y].GetSquareValue() == "0";
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

        private void InitialiseBoard()
        {
            if (_mineCoordinates != null)
            {
                foreach (var mine in _mineCoordinates)
                {
                    _board[mine.X, mine.Y] = new MineSquare();
                }
            }

            for (var row = 0; row < Dimension.NumRows; row++)
            {
                for (var col = 0; col < Dimension.NumCols; col++)
                {
                    if (_board[row, col] == null) _board[row, col] = new SafeSquare();
                }
            }
        }

        private void CalculateHints()
        {
            for (var row = 0; row < Dimension.NumRows; row++)
            {
                for (var col = 0; col < Dimension.NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate, Dimension);
                    var mineCount = GetNumberOfAdjacentMines(adjacentSquaresList);
                    if (mineCount > 0) _board[row, col].AddHint(mineCount);
                }
            }
        }

        private int GetNumberOfAdjacentMines(List<Coordinate> AdjacentSquaresList)
        {
            var mineCount = 0;
            foreach (var coord in AdjacentSquaresList)
            {
                if (SquareHasMine(coord)) mineCount++;
            }
            return mineCount;
        }
    }
}