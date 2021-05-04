using System;

namespace Minesweeper
{
    public enum GameResult
    {
        LOSE, WIN
    }

    public class GameController
    {
        private IIO _io;
        private Field _field;
        private IBuild _builder;

        public GameController(IIO io, IBuild builder)
        {
            _io = io;
            _builder = builder;
        }

        public void Run()
        {
            SetUpGame();
            PlayGame();
            DisplayResults();
        }

        public void SetUpGame()
        {
            _io.WriteLine("Welcome to Tic Tac Toe");
            _io.Write("Please enter the dimensions of your field row,column: ");
            string playerDimensionInput = _io.ReadLine();
            bool validInput = false;

            while (!validInput)
            {
                if (Validation.IsFieldDimensionInputValid(playerDimensionInput))
                {
                    validInput = true;
                }
                else
                {
                    _io.Write("Sorry, those dimensions aren't valid. Please enter other dimensions: ");
                    playerDimensionInput = _io.ReadLine();
                }
            }

            var dimension = TurnInputIntoDimension(playerDimensionInput);

            _field = _builder.CreateField(dimension);

            _io.DisplayField(_field.GetField(), _field.Dimension);
        }

        public void PlayGame()
        {
            bool gameHasEnded = false;

            while (!gameHasEnded)
            {
                _io.Write("Please enter a coordinate x,y: ");
                var playerInput = GetValidInput();
                if (playerInput == "q")
                {
                    break;
                }

                var coord = ConvertInputIntoCorrespondingCoordinate(playerInput);

                Rules.SetCoordinatesToShow(_field, coord);

                _io.DisplayField(_field.GetField(), _field.Dimension);

                if (GameHasEnded(coord)) gameHasEnded = true;
            }
        }

        private Coordinate ConvertInputIntoCorrespondingCoordinate(string input)
        {
            var coordinateArray = input.Split(',');

            var x = Int32.Parse(coordinateArray[0]) - 1;
            var y = Int32.Parse(coordinateArray[1]) - 1;

            return new Coordinate(x, y);
        }

        private string GetValidInput()
        {
            var validInput = false;
            string playerCoordinateInput = _io.ReadLine();

            while (!validInput)
            {
                if (Validation.IsCoordinateInputValid(_field.Dimension, playerCoordinateInput))
                {
                    validInput = true;
                }
                else
                {
                    _io.Write("Sorry, those coordinates aren't valid. Please enter another coordinate: ");
                    playerCoordinateInput = _io.ReadLine();
                }
            }

            return playerCoordinateInput;
            
        }

        private bool GameHasEnded(Coordinate coord)
        {
            if (Rules.CoordinateHasMineSquare(_field, coord) || Rules.RemainingSquaresAreMines(_field))
            {
                return true;
            }

            return false;
        }

        private Dimension TurnInputIntoDimension(string input)
        {
            var dimensionArray = input.Split(',');

            int row, column;

            var rowResult = Int32.TryParse(dimensionArray[0], out row);
            var columnResult = Int32.TryParse(dimensionArray[1], out column);

            return new Dimension(row, column);
        }

        public GameResult GameResult(Field field)
        {
            if (Rules.RemainingSquaresAreMines(field)) return Minesweeper.GameResult.WIN;
            else return Minesweeper.GameResult.LOSE;
        }

        public void DisplayResults()
        {
            if (GameResult(_field) == Minesweeper.GameResult.WIN)
            {
                _io.WriteLine("You've won the game :)");
            } else
            {
                _io.WriteLine("You've lost :(");
            }
        }
    }
}
