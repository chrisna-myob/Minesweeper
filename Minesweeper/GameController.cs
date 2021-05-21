using System.Collections.Generic;
using Minesweeper.Build;

namespace Minesweeper
{
    public enum GameStatus
    {
        QUIT, LOSE, WIN, PLAY
    }

    public class GameController
    {
        private IIO _io;
        private Field _field;
        private IBuild _builder;
        private const string QUIT_GAME = "q";

        private readonly Dictionary<GameStatus, string> ResultMessage = new Dictionary<GameStatus, string>()
        {
            { GameStatus.QUIT, "You have quit the game." },
            { GameStatus.WIN, "You've won the game :)" },
            { GameStatus.LOSE, "You've lost :(" },
        };

        public GameController(IIO io, IBuild builder)
        {
            _io = io;
            _builder = builder;
        }

        public void Run()
        {
            DisplayMessage("Welcome to Minesweeper!");
            DisplayMessage("To play the game, enter in coordinates, starting from 1,1, to reveal a square. If the square is a mine, you lose the game, but if the remaining squares are all mines, you win!");
            SetUpGame();
            DisplayRevealedField();
            var result = Play();
            DisplayResults(result);
        }

        private void DisplayMessage(string message)
        {
            _io.WriteLine(message);
        }

        public void SetUpGame()
        {
            _field = GameSetup.SetupField(_io, _builder);
        }

        public GameStatus Play()
        {
            while (true)
            {
                try
                {
                    _io.Write("Please enter a coordinate x,y or q to quit: ");
                    var userInput = GetInput();
                    var status = GameRound.Round(userInput, _field);
                    if (status != GameStatus.PLAY) return status;
                    DisplayField();
                }
                catch (InvalidInputException exception)
                {
                    _io.WriteLine(exception.Message);
                }
            }
        }

        private void DisplayField()
        {
            _io.Write(_field.ToString());
        }

        private void DisplayRevealedField()
        {
            _io.DisplayRevealedField(_field.GetBoard(), _field.Dimension);
        }

        private string GetInput()
        {
            return _io.ReadLine();
        }

        private void DisplayResults(GameStatus result)
        {
            _io.WriteLine(ResultMessage[result]);
        }
    }
}
