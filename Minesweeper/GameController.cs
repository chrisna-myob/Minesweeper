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
            { GameResult.QUIT, "You have quit the game." },
            { GameResult.WIN, "You've won the game :)" },
            { GameResult.LOSE, "You've lost :(" },
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

        private void PrintWelcomeMessage()
        {
            _io.WriteLine("Welcome to Minesweeper!");
        }

        private void PrintInstructions()
        {
            _io.WriteLine("To play the game, enter in coordinates, starting from 1,1, to reveal a square. If the square is a mine, you lose the game, but if the remaining squares are all mines, you win!");
        }

        private void SetUpGame()
        {
            while (true)
            {
                try
                {
                    _io.Write("Please enter the dimensions of your field row,column: ");
                    var userInput = GetInput();

                    var dimension = DimensionBuilder.Make(userInput);

                    _field = _builder.CreateField(dimension);

                    return;
                }
                catch (InvalidInputException exception)
                {
                    _io.WriteLine(exception.Message);
                }
            }
        }

        private GameResult Play()
        {
            while (true)
            {
                try
                {
                    _io.Write("Please enter a coordinate x,y or q to quit: ");
                    var userInput = GetInput();
                    if (userInput == QUIT_GAME)
                    {
                        return GameResult.QUIT;
                    } else
                    {
                        var coord = CoordinateBuilder.MakeCoordinate(userInput, _field.Dimension);
                        ProcessCoordinate(coord);
                        DisplayField();
                        if (Rules.GameHasEnded(_field))
                        {
                            if (Rules.HasWon(_field)) return GameResult.WIN;
                            else return GameResult.LOSE;
                        }
                    }
                    
                }
                catch (InvalidInputException exception)
                {
                    _io.WriteLine(exception.Message);
                }
            }
        }

        private void ProcessCoordinate(Coordinate coord)
        {
            Rules.CoordinateIsUnique(_field, coord);
            SetCoordinateInFieldToShow(coord);
        }

        private void SetCoordinateInFieldToShow(Coordinate coord)
        {
            if (Rules.CanShowIndividualCoordinateInField(_field, coord))
            {
                _field.SetSquareToShowWithCoordinate(coord);
            } else
            {
                _field.SetAdjacentCoordinatesInFieldToShow(coord);
            }
        }

        private void DisplayField()
        {
            _io.DisplayField(_field.GetField(), _field.Dimension);
        }

        private void DisplayRevealedField()
        {
            _io.DisplayRevealedField(_field.GetField(), _field.Dimension);
        }

        private string GetInput()
        {
            return _io.ReadLine();
        }

        private void DisplayResults(GameResult result)
        {
            _io.WriteLine(ResultMessage[result]);
        }
    }
}
