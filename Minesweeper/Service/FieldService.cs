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

        public bool CanShowSquare(Coordinate coord)
        {
            return _fieldRepo.CanShowSquare(coord);
        }

        public Dimension GetDimension()
        {
            return _fieldRepo.GetDimension();
        }

        public void SetAdjacentCoordinatesInFieldToShow(Coordinate coordinate)
        {
            if (_fieldRepo.CanShowSquare(coordinate)) return;
            else
            {
                _fieldRepo.SetSquareToShow(coordinate);

                if (_fieldRepo.CoordinateHasHintLargerThanZero(coordinate)) return;
                else
                {
                    var adjacentSquaresList = GlobalHelpers.GetAdjacentCoordinates(coordinate.X, coordinate.Y, _fieldRepo.GetDimension());

                    foreach (var coord in adjacentSquaresList)
                    {
                        SetAdjacentCoordinatesInFieldToShow(coord);
                    }
                }
            }
        }

        public override string ToString()
        {
            var dimension = _fieldRepo.GetDimension();
            var lineBreak = Lines(dimension.NumCols);
            var stringBuilder = lineBreak;
            for (var row = 0; row < dimension.NumRows; row++)
            {
                stringBuilder += "|";
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    var coord = new Coordinate(row, col);
                    if (_fieldRepo.CanShowSquare(coord))
                    {
                        if (_fieldRepo.GetSquareValue(coord) == "0")
                        {
                            stringBuilder += "   |";
                        }
                        else
                        {
                            stringBuilder += $" {_fieldRepo.GetSquareValue(coord)} |";
                        }
                    }
                    else
                    {
                        stringBuilder += " . |";
                    }

                }
                stringBuilder += Environment.NewLine;
                stringBuilder += lineBreak;
            }

            return stringBuilder + " \n";
        }

        public string UncoveredBoardToString()
        {
            var dimension = _fieldRepo.GetDimension();
            var lineBreak = Lines(dimension.NumCols);
            var stringBuilder = lineBreak;
            for (var row = 0; row < dimension.NumRows; row++)
            {
                stringBuilder += "|";
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    var coord = new Coordinate(row, col);
                    stringBuilder += $" {_fieldRepo.GetSquareValue(coord)} |";
                }
                stringBuilder += Environment.NewLine;
                stringBuilder += lineBreak;
            }

            return stringBuilder + " \n";
        }

        private string Lines(int num)
        {
            var stringBuilder = " ";
            for (var i = 0; i < num; i++)
            {
                stringBuilder += "---";
            }

            for (var i = 0; i < num - 1; i++)
            {
                stringBuilder += "-";
            }

            return stringBuilder += " \n";
        }

        public bool RemainingSquaresAreMines()
        {
            var countOfMines = 0;
            var dimension = _fieldRepo.GetDimension();
            for (var row = 0; row < dimension.NumRows; row++)
            {
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    var coordinate = new Coordinate(row, col);
                    if (_fieldRepo.CanShowSquare(coordinate) == false)
                    {
                        if (_fieldRepo.CoordinateHasMine(coordinate) == true) countOfMines++;
                        else if (_fieldRepo.CoordinateHasMine(coordinate) == false) return false;
                    }

                }
            }
            if (countOfMines == _fieldRepo.NumberOfMines()) return true;
            return false;
        }

        public bool MineHasBeenUncovered()
        {
            var mineCoordinates = _fieldRepo.GetMineCoordinates();
            foreach (var coord in mineCoordinates)
            {
                if (_fieldRepo.CanShowSquare(coord)) return true;
            }
            return false;
        }
    }
}
