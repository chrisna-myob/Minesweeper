using System;

namespace Minesweeper
{
    public class FieldService : IFieldService
    {
        private readonly IFieldRepository _fieldRepo;

        public FieldService(IFieldRepository fieldRepo)
        {
            _fieldRepo = fieldRepo;
        }

        public Dimension GetDimension()
        {
            return _fieldRepo.GetDimension();
        }

        public void HandleCoordinate(Coordinate coord)
        {
            _fieldRepo.SetCoordinateToShow(coord);
        }

        public string UncoveredBoardToString()
        {
            return _fieldRepo.UncoveredBoardToString();
        }

        public string BoardToString()
        {
            return _fieldRepo.BoardToString();
        }

        public bool HasWon()
        {
            if (_fieldRepo.RemainingSquaresAreMines()) return true;
            else return false;
        }

        public bool HasLost()
        {
            if (_fieldRepo.MineHasBeenUncovered()) return true;
            else return false;
        }

        public void CoordinateHasAlreadyBeenUsed(Coordinate coord)
        {
            if (_fieldRepo.CanShowSquare(coord))
            {
                throw new InvalidInputException("You have already entered this coordinate.\n");
            }
        }
    }
}
