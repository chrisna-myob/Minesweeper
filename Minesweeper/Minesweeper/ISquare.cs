using System;
namespace Minesweeper
{
    public interface ISquare
    {
        string InitialCharacter { get; }
        bool CanShow { get; }
        bool HasMine();
        void SetSquareToShow();
        void AddHint(int hint);
        string RevealSquare();
    }
}
