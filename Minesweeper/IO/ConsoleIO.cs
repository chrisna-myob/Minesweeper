using System;
using System.Collections.Generic;

namespace Minesweeper.Repository
{
    public class ConsoleIO : IIO
    {
        public Dictionary<char, ConsoleColor> characterColour = new Dictionary<char, ConsoleColor>() {
            { '1', ConsoleColor.DarkBlue },
            { '2', ConsoleColor.DarkGreen },
            { '3', ConsoleColor.Red },
            { '4', ConsoleColor.Magenta },
            { '5', ConsoleColor.DarkRed },
            { '6', ConsoleColor.DarkCyan },
            { '7', ConsoleColor.DarkMagenta },
            { '8', ConsoleColor.DarkGray }
        };

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void DisplayGrid(string message)
        {
            foreach(var letter in message)
            {
                if (!characterColour.ContainsKey(letter)) Console.ResetColor();
                else
                {
                    Console.ForegroundColor = characterColour[letter];
                }
                Console.Write(letter);
            }
        }

        public string GetUserInput()
        {
            return Console.ReadLine().Trim();
        }
    }
}
