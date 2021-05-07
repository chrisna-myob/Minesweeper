using System;
namespace Minesweeper
{
    public class ConsoleIO : IIO
    {
        private const string COVERED_SQUARE = ".";
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.Write(message);
        }

        public void WriteLine()
        {
            Console.WriteLine();
        }

        public void WriteLine(string message)
        {
            Console.WriteLine(message);
        }

        public void DisplayField(ISquare[,] field, Dimension dimension)
        {
            for (var row = 0; row < dimension.NumRows; row++)
            {
                for (var col = 0; col < dimension.NumCols; col++)
                {
                    if (field[row, col].CanShow)
                    {
                        Write(field[row, col].RevealSquare());
                    }
                    else
                    {
                        Write(COVERED_SQUARE);
                    }
                }
                WriteLine();
            }
        }
    }
}
