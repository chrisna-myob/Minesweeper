using System;

namespace Minesweeper
{
    public class Dimension
    {
        private int _numRows;
        private int _numCols;

        public int NumRows => _numRows;
        public int NumCols => _numCols;

        public Dimension(int numRow, int numCol)
        {
            _numRows = numRow;
            _numCols = numCol;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Dimension d = (Dimension)obj;
                return (NumRows == d.NumRows) && (NumCols == d.NumCols);
            }
        }

        public override int GetHashCode()
        {
            return 0;
        }
    }
}
