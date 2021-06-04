using System;

namespace Minesweeper.Repository
{
    public class ConsoleOutputRepository : IOutputRepository
    {
        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayBoard(string message)
        {
            foreach(var letter in message)
            {
                if (letter == '1') Console.ForegroundColor = ConsoleColor.DarkBlue;
                else if (letter == '2') Console.ForegroundColor = ConsoleColor.DarkGreen;
                else if (letter == '3') Console.ForegroundColor = ConsoleColor.Red;
                else if (letter == '4') Console.ForegroundColor = ConsoleColor.DarkMagenta;
                else if (letter == '5') Console.ForegroundColor = ConsoleColor.DarkRed;
                else if (letter == '6') Console.ForegroundColor = ConsoleColor.DarkCyan;
                else if (letter == '7') Console.ForegroundColor = ConsoleColor.DarkMagenta;
                else if (letter == '8') Console.ForegroundColor = ConsoleColor.DarkGray;
                else Console.ResetColor();
                Console.Write(letter);
            }
        }
    }
}
