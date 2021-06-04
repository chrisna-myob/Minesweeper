using System;

namespace Minesweeper
{
    public interface IOutputRepository
    {
        void Write(string message);
        void WriteLine(string message);
        void DisplayBoard(string message);
    }
}