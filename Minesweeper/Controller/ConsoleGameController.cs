using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public class ConsoleGameController
    {
        private GameService _gameService;
        private GameState _state;

        public ConsoleGameController(GameService gameService)
        {
            _gameService = gameService;
            _state = GameState.PLAY;
        }

        public void Run()
        {
            _gameService.DisplayMessage(Messages.Welcome);
            SetUpLoop();
            GameLoop();
            _gameService.DisplayMessage(Messages.gameResult[_state]);
        }

        private void SetUpLoop()
        {
            while (true)
            {
                try
                {
                    _gameService.DisplayMessage(Messages.Difficulty);
                    var difficultyInput = _gameService.GetUserInput();
                    var difficultyLevel = _gameService.GetDifficulty(difficultyInput);

                    _gameService.DisplayMessage(Messages.EnterDimension);
                    var input = _gameService.GetUserInput();

                    _gameService.SetUpField(difficultyLevel, input);
                    _gameService.DisplayBoard();
                    break;
                }
                catch (InvalidInputException exception)
                {
                    _gameService.DisplayMessage(exception.Message);
                }
            }
        }

        private void GameLoop()
        {
            while (_state == GameState.PLAY)
            {
                try
                {
                    _gameService.DisplayMessage(Messages.EnterCoordinate);
                    var input = _gameService.GetUserInput();
                    if (input == "a") _gameService.DisplayUncoveredBoard();
                    else
                    {
                        _state = _gameService.GameRound(input);
                        _gameService.DisplayBoard();
                    }
                    
                }
                catch (InvalidInputException exception)
                {
                    _gameService.DisplayMessage(exception.Message);
                }
            }
        }
    }
}
