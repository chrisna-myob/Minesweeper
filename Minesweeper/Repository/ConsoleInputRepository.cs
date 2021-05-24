using System;

namespace Minesweeper.Repository
{
    public class ConsoleInputRepository : IInputRepository
    {
        public string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}
