using System;
using System.Collections.Generic;
using Minesweeper.Build;

namespace Minesweeper
{
    public enum GameResult
    {
        QUIT, LOSE, WIN
    }

    public class GameController
    {
        private IIO _io;
        private Field _field;
        private IBuild _builder;
        private const string QUIT_GAME = "q";

        private readonly Dictionary<GameResult, string> ResultMessage = new Dictionary<GameResult, string>()
        {
            { Minesweeper.GameResult.QUIT, "You have quit the game." },
            { Minesweeper.GameResult.WIN, "You've won the game :)" },
            { Minesweeper.GameResult.LOSE, "You've lost :(" },
        };

        public GameController(IIO io, IBuild builder)
        {
            _io = io;
            _builder = builder;
        }

        public void Run()
        {
            PrintWelcomeMessage();
            SetUpGame();
            PrintInstructions();
            DisplayRevealedField();
            var result = Play();
            DisplayResults(result);
        }

        public void PrintWelcomeMessage()
        {
            _io.WriteLine("Welcome to Minesweeper!");
        }

        public void PrintInstructions()
        {
            _io.WriteLine("To play the game, enter in coordinates, starting from 1,1, to reveal a square. If the square is a mine, you lose the game, but if the remaining squares are all mines, you win!");
        }

        public void SetUpGame()
        {
            while (true)
            {
                try
                {
                    _io.Write("Please enter the dimensions of your field row,column: ");
                    var userInput = GetInput();

                    var dimension = DimensionBuilder.Make(userInput);

                    _field = _builder.CreateField(dimension);

                    break;
                }
                catch (InvalidInputException exception)
                {
                    DisplayException(exception);
                }
            }

            
        }

        public GameResult Play()
        {
            bool gameHasEnded = false;

            while (!gameHasEnded)
            {
                try
                {
                    _io.Write("Please enter a coordinate x,y or q to quit: ");
                    var userInput = GetInput();
                    if (userInput == QUIT_GAME)
                    {
                        return Minesweeper.GameResult.QUIT;
                    } else
                    {
                        var coord = CoordinateBuilder.MakeCoordinate(userInput, _field.Dimension);
                        Rules.SetCoordinatesToShow(_field, coord);
                        DisplayField();
                        if (GameHasEnded(coord)) gameHasEnded = true;
                    }
                    
                }
                catch (InvalidInputException exception)
                {
                    DisplayException(exception);
                }
            }

            return GameResult();
        }

        public void DisplayField()
        {
            _io.DisplayField(_field.GetField(), _field.Dimension);
        }

        public void DisplayRevealedField()
        {
            _io.DisplayRevealedField(_field.GetField(), _field.Dimension);
        }

        public string GetInput()
        {
            return _io.ReadLine();
        }

        private bool GameHasEnded(Coordinate coord)
        {
            if (Rules.CoordinateHasMineSquare(_field, coord) || Rules.RemainingSquaresAreMines(_field))
            {
                return true;
            }

            return false;
        }

        public GameResult GameResult()
        {
            if (Rules.RemainingSquaresAreMines(_field)) return Minesweeper.GameResult.WIN;
            return Minesweeper.GameResult.LOSE;
        }

        public void DisplayResults(GameResult result)
        {
            _io.WriteLine(ResultMessage[result]);
        }

        public void DisplayException(Exception exception)
        {
            _io.WriteLine(exception.Message);
        }
    }
}
