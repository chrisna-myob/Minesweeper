using System;
namespace Minesweeper
{
    public interface ISquare
    {
        bool CanShow { get; }
        bool HasMine();
        void SetSquareToShow();
        void AddHint(int hint);
        string RevealSquare();
    }
}
