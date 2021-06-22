using System;
using Minesweeper.Factory;
using Minesweeper.Repository;
using Minesweeper.Service;

namespace Minesweeper
{
    public static class ClassInstanceFactory
    {
        public static ConsoleGameController GetGameControllerInstance()
        {
            var gameService = GetGameServiceInstance();
            return new ConsoleGameController(gameService, CreateIOClass());
        }

        private static GameService GetGameServiceInstance()
        {
            var coordinateService = CreateCoordinateServiceClass();

            return new GameService(
                CreateFieldService(coordinateService),
                CreateDimensionFactoryClass(),
                CreateCoordinateFactoryClass(),
                CreateValidationClass(),
                CreateMineCoordinateFactoryClass(),
                CreateGridFactoryClass(coordinateService)
            );
        }

        private static CoordinateService CreateCoordinateServiceClass()
        {
            return new CoordinateService();
        }

        private static GridFactory CreateGridFactoryClass(CoordinateService coordinateService)
        {
            return new GridFactory(coordinateService);
        }

        private static FieldService CreateFieldService(CoordinateService coordinateService)
        {
            return new FieldService(coordinateService);
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
