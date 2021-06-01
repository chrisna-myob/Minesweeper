using System;

namespace Minesweeper
{
    public interface IGameService
    {
        void InitialiseField(string difficulty, string userInput);
        string GetUserInput();
        void DisplayMessage(string message);
        void DisplayBoard();
        void DisplayUncoveredBoard();
        GameState GameRound(string input);
        void ValidateDifficulty(string difficulty);
    }
}
