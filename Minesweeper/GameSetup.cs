using System;
using Minesweeper.Build;

namespace Minesweeper
{
    public static class GameSetup
    {
        public static Field SetupField(IIO io, IBuild builder)
        {
            while (true)
            {
                try
                {
                    io.Write("Please enter the dimensions of your field row,column: ");
                    var userInput = io.ReadLine();

                    var dimension = DimensionBuilder.MakeDimension(userInput);

                    return builder.CreateField(dimension);

                }
                catch (InvalidInputException exception)
                {
                    io.WriteLine(exception.Message);
                }
            }
        }
    }
}
