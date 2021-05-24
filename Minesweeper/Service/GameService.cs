using Minesweeper.Model;
using Minesweeper.Repository;

namespace Minesweeper
{
    public class GameService : IGameService
    {
        private readonly IInputRepository _inputRepo;
        private readonly IOutputRepository _outputRepo;
        private IFieldService _fieldService;
        private FieldBuilder _builder;
        private DimensionRepository _dimensionRepo;
        private CoordinateRepository _coordinateRepo;


        public GameService(IInputRepository inputRepo, FieldBuilder builder, IOutputRepository outputRepo, DimensionRepository dimensionRepo, CoordinateRepository coordinateRepo)
        {
            _inputRepo = inputRepo;
            _builder = builder;
            _outputRepo = outputRepo;
            _dimensionRepo = dimensionRepo;
            _coordinateRepo = coordinateRepo;
        }

        public Dimension MakeDimension()
        {
            Dimension dimension;
            while (true)
            {
                try
                {
                    var input = _inputRepo.GetUserInput();

                    dimension = _dimensionRepo.MakeDimension(input);

                    return dimension;
                }
                catch (InvalidInputException exception)
                {
                    _outputRepo.WriteLine(exception.Message);
                }
            }
        }

        public string GetUserInput()
        {
            return _inputRepo.GetUserInput();
        }

        public void CreateFieldService(Dimension dimension)
        {
            var field = _builder.CreateField(dimension);
            var fieldRepo = new FieldRepository(field);
            _fieldService = new FieldService(fieldRepo);
        }

        public Coordinate MakeCoordinate(string input)
        {
            var dimension = _fieldService.GetDimension();
            var coord = _coordinateRepo.MakeCoordinate(input, dimension);
            Rules.CoordinateHasAlreadyBeenUsed(_fieldService, coord);
            return coord;
        }

        public GameState GameRound()
        {
            DisplayMessage(Messages.EnterCoordinate);
            var userInput = _inputRepo.GetUserInput();
            if (UserWantsToQuit(userInput)) return GameState.QUIT;
            HandleInput(userInput);
            DisplayBoard();
            return GetGameStatus();
        }

        public bool UserWantsToQuit(string input)
        {
            return input == "q";
        }

        public void DisplayMessage(string message)
        {
            _outputRepo.WriteLine(message);
        }

        public GameState GetGameStatus()
        {
            if (Rules.HasWon(_fieldService)) return GameState.WIN;
            if (Rules.GameHasEnded(_fieldService)) return GameState.LOSE;
            return GameState.PLAY;
        }

        public void DisplayUncoveredBoard()
        {
            DisplayMessage(_fieldService.UncoveredBoardToString());
        }
        
        public void DisplayBoard()
        {
            DisplayMessage(_fieldService.ToString());
        }

        public void HandleInput(string userInput)
        {
            var coord = MakeCoordinate(userInput);
            _fieldService.SetAdjacentCoordinatesInFieldToShow(coord);
        }
    }
}