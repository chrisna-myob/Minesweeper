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
            if (_fieldRepo.RemainingSquaresAreMines())
            {
                return true;
            }
            return false;
        }

        public bool GameHasEnded()
        {
            if (_fieldRepo.MineHasBeenUncovered() || _fieldRepo.RemainingSquaresAreMines())
            {
                return true;
            }
            return false;
        }

        public void CoordinateHasAlreadyBeenUsed(Coordinate coord)
        {
            if (_fieldRepo.CanShowSquare(coord))
            {
                throw new InvalidInputException("You have already entered this coordinate.");
            }
        }
    }
}
