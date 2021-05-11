using System;
namespace Minesweeper
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string message) : base(message)
        { }
    }
}
