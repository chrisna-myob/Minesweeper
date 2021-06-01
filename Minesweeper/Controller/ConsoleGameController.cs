using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public class ConsoleGameController
    {
        private readonly IGameService _gameService;
        private GameState _state;

        public ConsoleGameController(IGameService gameService)
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
                    _gameService.DisplayMessage("Please enter difficulty (EASY, INTERMEDIATE, EXPERT): ");
                    var difficulty = _gameService.GetUserInput();
                    _gameService.ValidateDifficulty(difficulty);

                    _gameService.DisplayMessage(Messages.EnterDimension);
                    var input = _gameService.GetUserInput();

                    _gameService.InitialiseField(difficulty, input);
                    _gameService.DisplayUncoveredBoard();
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
                    _state = _gameService.GameRound(input);
                    _gameService.DisplayBoard();
                }
                catch (InvalidInputException exception)
                {
                    _gameService.DisplayMessage(exception.Message);
                }
            }
        }
    }
}
