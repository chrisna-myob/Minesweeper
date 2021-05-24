using System.Collections.Generic;
using System;

namespace Minesweeper
{
    public class ConsoleGameController
    {
        private readonly IGameService _gameService;
        private GameState _state = GameState.PLAY;

        public ConsoleGameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        public void Run()
        {
            _gameService.DisplayMessage(Messages.Welcome);
            SetUpGame();
            GameLoop();
            _gameService.DisplayMessage(Messages.gameResult[_state]);
        }

        public void SetUpGame()
        {
            _gameService.DisplayMessage(Messages.EnterDimension);
            var dimension = _gameService.MakeDimension();
            _gameService.CreateFieldService(dimension);
            _gameService.DisplayUncoveredBoard();
        }

        private void GameLoop()
        {
            while (_state == GameState.PLAY)
            {
                try
                {
                    _state = _gameService.GameRound();
                }
                catch (InvalidInputException exception)
                {
                    _gameService.DisplayMessage(exception.Message);
                }
            }
        }
    }
}
