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

        public void InitialiseField(string userInput)
        {
            var dimension = _dimensionRepo.MakeDimension(userInput);
            CreateFieldService(dimension);
        }

        private void CreateFieldService(Dimension dimension)
        {
            var field = _builder.CreateField(dimension);
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
            _fieldService.SetAdjacentCoordinatesInFieldToShow(coord);
        }

        private Coordinate MakeCoordinate(string input)
        {
            var dimension = _fieldService.GetDimension();
            var coord = _coordinateRepo.MakeCoordinate(input, dimension);
            Rules.CoordinateHasAlreadyBeenUsed(_fieldService, coord);
            return coord;
        }

        private GameState GetGameStatus()
        {
            if (Rules.HasWon(_fieldService)) return GameState.WIN;
            if (Rules.GameHasEnded(_fieldService)) return GameState.LOSE;
            return GameState.PLAY;
        }

        public void DisplayMessage(string message)
        {
            _outputRepo.WriteLine(message);
        }

        public void DisplayUncoveredBoard()
        {
            DisplayMessage(_fieldService.UncoveredBoardToString());
        }
        
        public void DisplayBoard()
        {
            DisplayMessage(_fieldService.ToString());
        }
    }
}