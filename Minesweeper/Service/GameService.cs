using Minesweeper.Model;
using Minesweeper.Repository;

namespace Minesweeper
{
    public class GameService
    {
        private readonly IInputRepository _inputRepo;
        private readonly IOutputRepository _outputRepo;
        private FieldService _fieldService;
        private DimensionFactory _dimensionFactory;
        private CoordinateFactory _coordinateFactory;
        private Validation _validation;
        private MineCoordinateFactory _mineCoordinateFactory;

        public GameService(IInputRepository inputRepo, IOutputRepository outputRepo, DimensionFactory dimensionFactory, CoordinateFactory coordinateFactory, Validation validation, MineCoordinateFactory mineCoordinateFactory)
        {
            _inputRepo = inputRepo;
            _outputRepo = outputRepo;
            _dimensionFactory = dimensionFactory;
            _coordinateFactory = coordinateFactory;
            _validation = validation;
            _mineCoordinateFactory = mineCoordinateFactory;
        }

        public DifficultyLevel GetDifficulty(string input)
        {
            _validation.IsDifficultyLevelValid(input);

            if (input == "EASY") return DifficultyLevel.EASY;
            if (input == "INTERMEDIATE") return DifficultyLevel.INTERMEDIATE;
            else return DifficultyLevel.EXPERT;
        }

        public string GetUserInput()
        {
            return _inputRepo.GetUserInput();
        }

        public void SetUpField(DifficultyLevel difficulty, string userDimension)
        {
            var dimension = _dimensionFactory.MakeDimension(userDimension, _validation);

            var coordinates = _mineCoordinateFactory.MakeUniqueMineCoordinates(difficulty, dimension);

            var field = new Field(dimension, coordinates);

            _fieldService = new FieldService(field);
        }

        public GameState GameRound(string userInput)
        {
            if (userInput == Messages.QUIT) return GameState.QUIT;
            HandleInput(userInput);
            return GetGameStatus();
        }

        private void HandleInput(string userInput)
        {
            var coord = MakeCoordinate(userInput);
            _fieldService.HandleCoordinate(coord);
        }

        private Coordinate MakeCoordinate(string input)
        {
            var dimension = _fieldService.GetDimension();
            var coord = _coordinateFactory.MakeCoordinate(dimension, input, _validation);
            _fieldService.CoordinateHasAlreadyBeenUncovered(coord);
            return coord;
        }

        private GameState GetGameStatus()
        {
            if (_fieldService.HasWon()) return GameState.WIN;
            if (_fieldService.HasLost()) return GameState.LOSE;
            return GameState.PLAY;
        }

        public void DisplayMessage(string message)
        {
            _outputRepo.Write(message);
        }

        public void DisplayUncoveredBoard()
        {
            _outputRepo.DisplayBoard(_fieldService.BoardToString(View.ADMIN));
        }
        
        public void DisplayBoard()
        {
            _outputRepo.DisplayBoard(_fieldService.BoardToString(View.PLAYER));
        }
    }
}