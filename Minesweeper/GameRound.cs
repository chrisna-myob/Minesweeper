using Minesweeper.Build;

namespace Minesweeper
{
    public static class GameRound
    {
        public static GameStatus Round(string userInput, Field field)
        {
            if (userInput == "q") return GameStatus.QUIT;
            else
            {
                HandleInput(userInput, field);
                if (Rules.HasWon(field)) return GameStatus.WIN;
                if (Rules.GameHasEnded(field)) return GameStatus.LOSE;
            }
            return GameStatus.PLAY;
        }

        public static void HandleInput(string userInput, Field field)
        {
            var coord = CoordinateBuilder.MakeCoordinate(userInput, field.Dimension);
            Rules.ValidateCoordinate(field, coord);
            SetCoordinateInFieldToShow(field, coord);
        }

        public static void SetCoordinateInFieldToShow(Field field, Coordinate coord)
        {
            if (Rules.CanShowIndividualCoordinateInField(field, coord))
            {
                field.SetSquareToShowWithCoordinate(coord);
            }
            else
            {
                field.SetAdjacentCoordinatesInFieldToShow(coord);
            }
        }
    }
}
