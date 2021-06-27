using System;

namespace Minesweeper
{
    public interface ISquare
    {
        bool HasBeenUncovered { get; }
        bool HasMine();
        void Uncover();
        void AddHint(int hint);
        string GetSquareValue();
        string GetSquareAsString(View view);
    }
}
