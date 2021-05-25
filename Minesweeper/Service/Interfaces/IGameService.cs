using System;

namespace Minesweeper
{
    public interface IGameService
    {
        void InitialiseField(string userInput);
        string GetUserInput();
        void DisplayMessage(string message);
        void DisplayBoard();
        void DisplayUncoveredBoard();
        GameState GameRound(string input);
    }
}
