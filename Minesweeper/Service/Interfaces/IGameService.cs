using System;

namespace Minesweeper
{
    public interface IGameService
    {
        Dimension MakeDimension();
        void CreateFieldService(Dimension dimension);
        string GetUserInput();
        bool UserWantsToQuit(string input);
        void DisplayMessage(string message);
        void HandleInput(string userInput);
        void DisplayBoard();
        void DisplayUncoveredBoard();
        GameState GetGameStatus();
        GameState GameRound();
    }
}
