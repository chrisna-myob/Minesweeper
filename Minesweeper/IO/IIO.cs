using System;
namespace Minesweeper
{
    public interface IIO
    {
        void Write(string message);

        void DisplayGrid(string message);

        string GetUserInput();
    }
}
