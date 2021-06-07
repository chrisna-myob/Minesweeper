using System;

namespace Minesweeper
{
    public interface ISquare
    {
        bool CanBeDisplayed { get; }
        bool HasMine();
        void Uncover();
        void AddHint(int hint);
        string GetSquareValue();
    }
}
