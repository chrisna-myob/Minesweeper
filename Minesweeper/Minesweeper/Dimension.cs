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
    }
}
