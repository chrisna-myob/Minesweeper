using System;
using System.Collections.Generic;
using Minesweeper.Repository;
using Minesweeper.Model;
using Moq;
using System.Linq;

namespace Minesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            var rng = new RandomNumberGenerator();
            var mineFactory = new MineCoordinateFactory(rng);
            var output = new ConsoleOutputRepository();
            var input = new ConsoleInputRepository();
            var dimension = new DimensionFactory();
            var coordinate = new CoordinateFactory();
            var validation = new Validation();
            var service = new GameService(input, output, dimension, coordinate, validation, mineFactory);
            var game = new ConsoleGameController(service);
            game.Run();
        }
    }
}
