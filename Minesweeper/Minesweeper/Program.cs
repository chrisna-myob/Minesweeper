using System;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var console = new ConsoleIO();
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var gameController = new GameController(console, builder);
            gameController.Run();
        }
    }
}
