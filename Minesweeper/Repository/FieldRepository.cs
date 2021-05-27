using System.Collections.Generic;
using System;

namespace Minesweeper.Repository
{
    public class FieldRepository : IFieldRepository
    {
        private Field _field;

        public FieldRepository(Field field)
        {
            _field = field;
        }

        public Dimension GetDimension()
        {
            return _field.Dimension;
        }

        public List<Coordinate> GetMineCoordinates()
        {
            return _field.GetMineCoordinates();
        }

        public int NumberOfMines()
        {
            return _field.NumberOfMines;
        }

        public bool CanShowSquare(Coordinate coord)
        {
            return _field.CanShowSquare(coord);
        }

        public string GetSquareValue(Coordinate coord)
        {
            return _field.GetSquareValue(coord);
        }

        public bool CoordinateHasMine(Coordinate coord)
        {
            return _field.CoordinateHasMine(coord);
        }

        public void SetSquareToShow(Coordinate coord)
        {
            _field.SetSquareToShowWithCoordinate(coord);
        }

        public bool CoordinateHasHintLargerThanZero(Coordinate coord)
        {
            return _field.CoordinateHasHintLargerThanZero(coord);
        }
    }
}
