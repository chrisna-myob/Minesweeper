using System;
using Minesweeper.Repository;
using Minesweeper.Model;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var rng = new RandomNumberGenerator();
            var builder = new FieldBuilder(rng);
            var output = new ConsoleOutputRepository();
            var input = new ConsoleInputRepository();
            var dimension = new DimensionRepository();
            var coordinate = new CoordinateRepository();
            var service = new GameService(input, builder, output, dimension, coordinate);
            var game = new ConsoleGameController(service);
            game.Run();
        }
    }
}
