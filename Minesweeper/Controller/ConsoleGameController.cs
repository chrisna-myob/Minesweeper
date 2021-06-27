using System;

namespace Minesweeper
{
    public class ConsoleGameController
    {
        private GameService _gameService;
        private IIO _io;
        private GameState _state;

        public ConsoleGameController(GameService gameService, IIO io)
        {
            _gameService = gameService;
            _io = io;
            _state = GameState.PLAY;
        }

        public void Run()
        {
            _io.Write(GlobalGameVariables.WelcomeMessage);
            SetUpLoop();
            GameLoop();
            _io.Write(GlobalGameVariables.gameResult[_state]);
        }

        private void SetUpLoop()
        {
            while (true)
            {
                try
                {
                    _io.Write(GlobalGameVariables.InputDifficultyMessage);
                    var difficultyInput = _io.GetUserInput();

                    _io.Write(GlobalGameVariables.InputDimensionMessage);
                    var input = _io.GetUserInput();

                    _gameService.SetUpField(difficultyInput, input);
                    var grid = _gameService.GetGrid(_state);
                    _io.DisplayGrid(grid);
                    break;
                }
                catch (InvalidInputException exception)
                {
                    _io.Write(exception.Message);
                }
            }
        }

        private void GameLoop()
        {
            while (_state == GameState.PLAY || _state == GameState.ADMIN)
            {
                try
                {
                    _io.Write(GlobalGameVariables.InputCoordinateMessage);
                    var input = _io.GetUserInput();
                    _state = _gameService.GameRound(input);
                    var grid = _gameService.GetGrid(_state);
                    _io.DisplayGrid(grid);
                }
                catch (InvalidInputException exception)
                {
                    _io.Write(exception.Message);
                }
            }
        }
    }
}
