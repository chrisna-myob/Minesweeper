using System;
using Minesweeper.Factory;
using Minesweeper.Repository;

namespace Minesweeper
{
    public static class ClassInstanceFactory
    {
        public static ConsoleGameController GetGameControllerInstance()
        {
            var gameService = GetGameServiceInstance();
            return new ConsoleGameController(gameService);
        }

        private static GameService GetGameServiceInstance()
        {
            return new GameService(
                CreateFieldService(),
                CreateIOClass(),
                CreateDimensionFactoryClass(),
                CreateCoordinateFactoryClass(),
                CreateValidationClass(),
                CreateMineCoordinateFactoryClass(),
                CreateGridFactoryClass()
            );
        }

        private static GridFactory CreateGridFactoryClass()
        {
            return new GridFactory();
        }

        private static FieldService CreateFieldService()
        {
            return new FieldService();
        }

        private static IIO CreateIOClass()
        {
            return new ConsoleIO();
        }

        private static DimensionFactory CreateDimensionFactoryClass()
        {
            return new DimensionFactory();
        }

        private static CoordinateFactory CreateCoordinateFactoryClass()
        {
            return new CoordinateFactory();
        }

        private static Validation CreateValidationClass()
        {
            return new Validation();
        }

        private static MineCoordinateFactory CreateMineCoordinateFactoryClass()
        {
            var rng = new RandomNumberGenerator();

            return new MineCoordinateFactory(rng);
        }
    }
}
