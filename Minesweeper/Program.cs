using System;
using System.Collections.Generic;
using Minesweeper.Repository;
using Minesweeper.Model;
using Minesweeper.Repository.Interfaces;
using Moq;

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
