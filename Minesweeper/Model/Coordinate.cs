using System;

namespace Minesweeper
{
    public class Coordinate
    {
        private int _x;
        private int _y;

        public int X => _x;
        public int Y => _y;

        public Coordinate(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public override bool Equals(Object obj)
        {
            if ((obj == null) || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                Coordinate c = (Coordinate)obj;
                return (X == c.X) && (Y == c.Y);
            }
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}