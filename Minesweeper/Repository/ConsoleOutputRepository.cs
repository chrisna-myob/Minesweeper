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
    }
}
