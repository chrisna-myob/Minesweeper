using System;
namespace Minesweeper.Repository
{
    public interface IIO
    {
        void Write(string message);

        void DisplayBoard(string message);

        string GetUserInput();
    }
}
