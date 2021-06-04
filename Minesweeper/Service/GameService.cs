using Minesweeper.Repository;
using Minesweeper.Repository.Interfaces;

namespace Minesweeper
{
    public class GameService : IGameService
    {
        private readonly IInputRepository _inputRepo;
        private readonly IOutputRepository _outputRepo;
        private IFieldService _fieldService;
        private FieldBuilder _builder;
        private IDimensionRepository _dimensionRepo;
        private ICoordinateRepository _coordinateRepo;

        public GameService(IInputRepository inputRepo, FieldBuilder builder, IOutputRepository outputRepo, IDimensionRepository dimensionRepo, ICoordinateRepository coordinateRepo)
        {
            _inputRepo = inputRepo;
            _builder = builder;
            _outputRepo = outputRepo;
            _dimensionRepo = dimensionRepo;
            _coordinateRepo = coordinateRepo;
        }

        public string GetUserInput()
        {
            return _inputRepo.GetUserInput();
        }

        public void InitialiseField(string difficulty, string userDimension)
        {
            var dimension = _dimensionRepo.MakeDimension(userDimension);
            CreateFieldService(difficulty, dimension);
        }

        private void CreateFieldService(string difficulty, Dimension dimension)
        {
            var field = _builder.CreateField(difficulty, dimension);
            var fieldRepo = new FieldRepository(field);
            _fieldService = new FieldService(fieldRepo);
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
            var coord = _coordinateRepo.MakeCoordinate(input, dimension);
            _fieldService.CoordinateHasAlreadyBeenUsed(coord);
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
            _outputRepo.DisplayBoard(_fieldService.UncoveredBoardToString());
        }
        
        public void DisplayBoard()
        {
            _outputRepo.DisplayBoard(_fieldService.BoardToString());
        }

        public void ValidateDifficulty(string difficulty)
        {
            Validation.IsDifficultyLevelValid(difficulty);
        }
    }
}