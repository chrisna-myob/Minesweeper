using System;
namespace Minesweeper
{
    public interface IIO
    {
        string ReadLine();

        void WriteLine(string message);

        void Write(string message);

        void DisplayField(ISquare[,] field, Dimension dimension);

        void DisplayRevealedField(ISquare[,] field, Dimension dimension);

        void DisplayCoordinates(Dimension dimension);
    }
}
