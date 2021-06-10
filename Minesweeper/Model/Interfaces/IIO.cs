using System;
namespace Minesweeper
{
    public interface IIO
    {
        void Write(string message);

        void DisplayBoard(string message);

        string GetUserInput();
    }
}
