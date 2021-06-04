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
        public bool CanShowSquare(Coordinate coord)
        {
            return _field.CanShowSquare(coord);
        }

        public void SetCoordinateToShow(Coordinate coord)
        {
            _field.SetAdjacentCoordinatesInFieldToShow(coord);
        }

        public bool RemainingSquaresAreMines()
        {
            return _field.RemainingSquaresAreMines();
        }

        public bool MineHasBeenUncovered()
        {
            return _field.MineHasBeenUncovered();
        }

        public string UncoveredBoardToString() {
            return _field.UncoveredBoardToString();
        }

        public string BoardToString() {
            return _field.ToString();
        }
    }
}
