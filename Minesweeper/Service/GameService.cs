using Minesweeper.Factory;
using Minesweeper.Repository;
using Minesweeper.Service;

namespace Minesweeper
{
    public class GameService
    {
        private FieldService _fieldService;
        private DimensionFactory _dimensionFactory;
        private CoordinateFactory _coordinateFactory;
        private Validation _validation;
        private MineCoordinateFactory _mineCoordinateFactory;
        private GridFactory _gridFactory;

        public GameService(FieldService fieldService, DimensionFactory dimensionFactory, CoordinateFactory coordinateFactory, Validation validation, MineCoordinateFactory mineCoordinateFactory, GridFactory gridFactory)
        {
            _dimensionFactory = dimensionFactory;
            _coordinateFactory = coordinateFactory;
            _validation = validation;
            _mineCoordinateFactory = mineCoordinateFactory;
            _fieldService = fieldService;
            _gridFactory = gridFactory;
        }

        public void SetUpField(string difficultyInput, string dimensionInput)
        {
            var difficultyLevel = GetDifficulty(difficultyInput);

            var dimension = _dimensionFactory.MakeDimension(dimensionInput, _validation);

            var mineCoordinates = _mineCoordinateFactory.MakeUniqueMineCoordinates(difficultyLevel, dimension);

            var grid = _gridFactory.MakeGrid(dimension, mineCoordinates);

            var _field = new Field(dimension, mineCoordinates, grid);

            _fieldService.SetField(_field);
        }

        private DifficultyLevel GetDifficulty(string userInput)
        {
            _validation.IsDifficultyLevelValid(userInput);

            switch (userInput)
            {
                case "EASY":
                    return DifficultyLevel.EASY;
                case "INTERMEDIATE":
                    return DifficultyLevel.INTERMEDIATE;
                case "EXPERT":
                    return DifficultyLevel.EXPERT;
                default:
                    return DifficultyLevel.EASY;
            }
        }

        public GameState GameRound(string userInput)
        {
            switch(userInput)
            {
                case GlobalGameVariables.QUIT:
                    return GameState.QUIT;
                case GlobalGameVariables.ADMIN:
                    return GameState.ADMIN;
                default:
                    HandleInput(userInput);
                    return GetGameStatus();
            }
        }

        public string GetBoard(GameState state)
        {
            var view = (state == GameState.ADMIN ? View.ADMIN : View.PLAYER);
            return _fieldService.BoardToString(view);
        }

        private void HandleInput(string userInput)
        {
            var coordinate = MakeCoordinate(userInput);
            _fieldService.SetAdjacentCoordinatesToBeUncovered(coordinate);
        }

        private Coordinate MakeCoordinate(string userInput)
        {
            var dimension = _fieldService.GetDimension();
            var coord = _coordinateFactory.MakeCoordinate(dimension, userInput, _validation);
            var result = _fieldService.HasCoordinateHasAlreadyBeenUncovered(coord);
            _validation.CoordinateHasAlreadyBeenUncovered(result);
            return coord;
        }

        private GameState GetGameStatus()
        {
            if (_fieldService.HasWon()) return GameState.WIN;
            else if (_fieldService.HasLost()) return GameState.LOSE;
            return GameState.PLAY;
        }
    }
}